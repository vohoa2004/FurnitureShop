using Azure;
using FurnitureShop.DAL.Entities;
using FurnitureShop.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.DAL.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(FurnitureShopContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await FindAll(false).ToListAsync();
        }

        public async Task<Order?> GetOrderById(int orderId)
        {
            return await FindByCondition(o => o.Id == orderId, false).FirstOrDefaultAsync();
        }

        public async Task<(List<Order>, int totalCount)> GetOrdersByUser(Guid accountId, int pageNo, int pageSize)
        {
            var query = _context.Set<Order>().AsQueryable();
            query = query.Where(o => o.CustomerId == accountId)
                        .Include(o => o.OrderLines)
                            .ThenInclude(ol => ol.Product)
                        .OrderByDescending(o => o.Id);
            var totalCount = await query.CountAsync();
            query = query.Skip((pageNo - 1) * pageSize)
                        .Take(pageSize);
            var list = await query.ToListAsync();
            return (list, totalCount);
        }

        public void CreateOrder(Order order) // use transaction, only commit when finish payment
        {
            Create(order);
        }

        public void DeleteOrder(Order order)
        {
            Delete(order);
        }

        public async Task SaveOrder()
        {
            await Save();
        }

        public void UpdateOrder(Order order)
        {
            Update(order);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task<IEnumerable<Order>> FindOrderByTransactionId(string transactionNo)
        {
            return await FindByCondition(o => o.TransactionId1 == transactionNo, true).ToListAsync();
        }
    }
}
