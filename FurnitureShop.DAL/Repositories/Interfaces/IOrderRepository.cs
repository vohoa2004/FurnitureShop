using FurnitureShop.DAL.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.DAL.Repositories.Interfaces
{
    public interface IOrderRepository 
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order?> GetOrderById(int orderId);
        Task<(List<Order>, int totalCount)> GetOrdersByUser(Guid accountId, int pageNo, int pageSize);
        Task<IDbContextTransaction> BeginTransactionAsync();
        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
        Task SaveOrder();
        Task<IEnumerable<Order>> FindOrderByTransactionId(string transactionNo);
    }
}
