using AutoMapper;
using AutoMapper.Internal;
using FurnitureShop.BLL.BusinessModels;
using FurnitureShop.BLL.Services.Interfaces;
using FurnitureShop.BLL.Utils;
using FurnitureShop.DAL.Entities;
using FurnitureShop.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FurnitureShop.BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IOrderService _orderService;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public PaymentService(IOrderService orderService, IMapper mapper, IOrderRepository orderRepository)
        {
            _orderService = orderService;
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public string CreatePayment(OrderModel orderModel, HttpContext context)
        {
            if (orderModel.PaymentMethod.Equals(PaymentMethod.VNPay))
            {
                DateTime now = DateTime.Now;
                var orderCode = GenerateTemporaryOrderCode(orderModel.CustomerId, now);
                return GetVNPayPaymentLink(orderCode, orderModel.TotalPrice, context);
            }
            return "";
        }

        public async Task ProcessOrder(OrderModel orderDto, VNPayModel vnPayModel)
        {

            var orderModel = _mapper.Map<OrderModel>(orderDto);

            // Thêm khóa để đảm bảo đồng bộ
            await _semaphore.WaitAsync();
            try
            {
                orderModel.TransactionTime1 = vnPayModel.vnp_PayDate;
                orderModel.TransactionId1 = vnPayModel.vnp_BankTranNo;
                orderModel.AmountTransaction1 = (double)vnPayModel.vnp_Amount / 100;
                if (orderModel.AmountTransaction1 >= orderModel.TotalPrice)
                {
                    orderModel.IsDonePaid = true;
                }

                // Tạo mới đơn hàng trong transaction
                await _orderService.CreateNewOrder(orderModel);
            }
            finally
            {
                _semaphore.Release();
            }

        }


        private string GetVNPayPaymentLink(string orderCode, double price, HttpContext context)
        {
            IConfiguration _configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

            DateTime dateTime = DateTime.UtcNow.AddHours(7);

            var ipAddress = VnPayUtils.GetIpAddress(context);

            var pay = new VnPayLibrary();

            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
            pay.AddRequestData("vnp_Amount", ((double)price * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", dateTime.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
            pay.AddRequestData("vnp_IpAddr", ipAddress);
            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
            pay.AddRequestData("vnp_OrderInfo", $"Thanh toan cho don hang {orderCode}");
            pay.AddRequestData("vnp_OrderType", "250000");
            pay.AddRequestData("vnp_TxnRef", orderCode.ToString());
            pay.AddRequestData("vnp_ReturnUrl", _configuration["Vnpay:UrlReturn"]);

            var paymentUrl =
                pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);

            return paymentUrl;
        }

        private string GenerateTemporaryOrderCode(Guid customerId, DateTime orderedTime)
        {
            // Sử dụng phần ngẫu nhiên của Guid và thời gian để tạo seed cho Random
            var seed = customerId.GetHashCode() ^ orderedTime.GetHashCode();
            var random = new Random(seed);

            // Tạo hai số ngẫu nhiên từ 0 đến 99999 và ghép lại thành chuỗi 10 chữ số
            var part1 = random.Next(10000, 99999).ToString();
            var part2 = random.Next(10000, 99999).ToString();

            // Ghép hai phần lại để tạo mã code 10 chữ số
            var orderCode = part1 + part2;

            return orderCode;
        }

        // them phan request is duplicate
        public async Task<bool> ReturnFromVNPay(VNPayModel vnPayResponse)
        {
            IConfiguration _configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();

            if (vnPayResponse != null)
            {
                var vnpay = new VnPayLibrary();

                foreach (PropertyInfo prop in vnPayResponse.GetType().GetProperties())
                {
                    string name = prop.Name;
                    object value = prop.GetValue(vnPayResponse, null);
                    string valueStr = value?.ToString() ?? string.Empty;
                    vnpay.AddResponseData(name, valueStr);
                }

                var vnpayHashSecret = _configuration["Vnpay:HashSecret"];
                bool validateSignature = vnpay.ValidateSignature(vnPayResponse.vnp_SecureHash, vnpayHashSecret);

                if (validateSignature)
                {
                    var existOrderWithTransationId = await _orderRepository.FindOrderByTransactionId(vnPayResponse.vnp_BankTranNo);
                    if (existOrderWithTransationId.Count() > 0)
                    {
                        throw new Exception("Duplicate transaction detected.");
                    }

                    if (vnPayResponse.vnp_ResponseCode == "00" && vnPayResponse.vnp_TransactionStatus == "00")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    throw new Exception("Wrong signature");
                }
            }
            else
            {
                throw new Exception("Error when return!");
            }
        }

    }
}
