using AutoMapper;
using FurnitureShop.BLL.BusinessModels;
using FurnitureShop.BLL.Services.Interfaces;
using FurnitureShop.DAL.Entities;
using FurnitureShop.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FurnitureShop.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderLineRepository _orderLineRepository;
        private readonly IFurnitureRepository _funitureRepository;
        private readonly IMapper _mapper;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public OrderService(IAccountRepository accountRepository, IOrderRepository orderRepository, IMapper mapper, IFurnitureRepository funitureRepository, IOrderLineRepository orderLineRepository)
        {
            _accountRepository = accountRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
            _funitureRepository = funitureRepository;
            _orderLineRepository = orderLineRepository;

        }
        public async Task CreateNewOrder(OrderModel orderModel)
        {
            // Sử dụng SemaphoreSlim để đảm bảo chỉ một thao tác có thể chạy tại một thời điểm
            await _semaphore.WaitAsync();
            try
            {
                using (var transaction = await _orderRepository.BeginTransactionAsync())
                {
                    try
                    {
                        var user = await _accountRepository.FindById(orderModel.CustomerId);
                        if (user == null)
                        {
                            throw new Exception("Không tìm thấy user");
                        }

                        // Tạo order add vào database
                        var order = _mapper.Map<Order>(orderModel);
                        order.Status = OrderStatus.Processing_From_Warehouse;
                        order.CreatedAt = DateTime.Now;
                        _orderRepository.CreateOrder(order);

                        foreach (var line in order.OrderLines)
                        {

                            var product = await _funitureRepository.FindFunitureByIdAsync(line.ProductId, false)
                                            ?? throw new KeyNotFoundException($"Product with ID = {line.ProductId} not found!");
                            line.ProductPrice = product.Price;
                            if (product.AvailableQuantity < line.Quantity)
                            {
                                throw new Exception($"Invalid quantity to buy product with ID = {line.ProductId}, available is less than buy quantity!");
                            }

                            // Giảm số lượng hàng trong kho
                            product.AvailableQuantity -= line.Quantity;
                            _funitureRepository.UpdateFurniture(product);

                            _orderLineRepository.CreateOrderLine(line);
                        }
                        await _orderRepository.SaveOrder();
                        await transaction.CommitAsync();
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }


        public async Task<IEnumerable<OrderModel>> GetAllOrders()
        {
            // paging sau
            var ordersDB = await _orderRepository.GetAllOrders();
            List<OrderModel> orderModels = _mapper.Map<List<OrderModel>>(ordersDB);
            return orderModels;
        }

        public async Task<(List<OrderModel>, int totalCount)> GetOrdersByUser(Guid accountId, int pageNo, int pageSize)
        {
            int totalCount = 0;
            (List<Order> ordersDB, totalCount) = await _orderRepository.GetOrdersByUser(accountId, pageNo, pageSize);
            List<OrderModel> orderModels = _mapper.Map<List<OrderModel>>(ordersDB);
            return (orderModels, totalCount);
        }

        public Task<IEnumerable<OrderModel>> GetOrdersById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
