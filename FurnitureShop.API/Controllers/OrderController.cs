using AutoMapper;
using FurnitureShop.API.DTOs.RequestDTOs;
using FurnitureShop.API.DTOs.ResponseDTOs;
using FurnitureShop.BLL.BusinessModels;
using FurnitureShop.BLL.Services.Interfaces;
using FurnitureShop.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;
        public OrderController(IOrderService orderService, IMapper mapper, IPaymentService paymentService)
        {
            _orderService = orderService;
            _mapper = mapper;
            _paymentService = paymentService;

        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder(OrderRequestDTO dto)
        {
            // Neu payment method = offline => luu thong tin order + order status = shipping
            // Neu payment method = online => lay thong tin order => goi api create payment do ben thu 3 cung cap => response link payment
            try
            {
                var orderModel = _mapper.Map<OrderModel>(dto);
                if (PaymentMethod.Offline.Equals(dto.PaymentMethod))
                {
                    await _orderService.CreateNewOrder(orderModel);
                    return Ok("Created new order!");
                }
                else
                // Chua tru so luong, chua luu order, order chi luu o cookie o browser thoi
                {
                    var paymentLink = _paymentService.CreatePayment(orderModel, HttpContext);
                    return Ok(paymentLink);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ProcessPaymentAfterCheckingStatus")]
        public async Task<IActionResult> ProcessPaymentAfterCheckingStatus([FromQuery] VNPayModel vnPayModel, [FromBody] OrderRequestDTO orderDto)
        {
            try
            {
                // goi service check payment do ben thu 3 cung cap
                // success => tru so luong tung item theo tung line trong orderDTO + luu order vao DB + response OK
                await _paymentService.ProcessOrder(_mapper.Map<OrderModel>(orderDto), vnPayModel);

                // failed => ko lam gi ca ma response Bad Request voi li do la failed hay j do cho FE de FE xoa order trong cookie va thong bao cho nguoi dung
                return Ok("Created new order");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("CheckPaymentStatus")]
        public async Task<IActionResult> CheckPaymentStatus([FromQuery] VNPayModel vnPayModel)
        {
            try
            {
                // goi service check payment do ben thu 3 cung cap
                // success => tru so luong tung item theo tung line trong orderDTO + luu order vao DB + response OK
                bool paymentStatus = await _paymentService.ReturnFromVNPay(vnPayModel);
                if (paymentStatus)
                {

                    // failed => ko lam gi ca ma response Bad Request voi li do la failed hay j do cho FE de FE xoa order trong cookie va thong bao cho nguoi dung
                    return Ok("Payment succeeded");
                } else
                {
                    return BadRequest("Payment failed!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrders();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersByCutomer(Guid customerId, [FromQuery] int pageNo, int pageSize)
        {
            try
            {
                int totalCount = 0;
                (List<OrderModel> orders, totalCount) = await _orderService.GetOrdersByUser(customerId, pageNo, pageSize);
                var responseData = new PaginationDTO<OrderModel>(orders, totalCount, pageNo, pageSize);
                return Ok(responseData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
