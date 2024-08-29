using FurnitureShop.DAL.Entities;
using FurnitureShop.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.DAL.Repositories
{
    public class OrderLineRepository : GenericRepository<OrderLine>, IOrderLineRepository
    {
        public OrderLineRepository(FurnitureShopContext context) : base(context)
        {
        }

        public async Task<IEnumerable<OrderLine>> GetOrderLinesByOrderId(int orderId)
        {
            return await FindByCondition(o => o.OrderId == orderId, false).ToListAsync();
        }

        public void CreateOrderLine(OrderLine orderLine) // use transaction, only commit when finish payment
        {
            Create(orderLine);
        }

        public void DeleteOrderLine(OrderLine orderLine)
        {
            Delete(orderLine);
        }

        public async Task SaveOrderLine()
        {
            await Save();
        }

        public void UpdateOrderLine(OrderLine orderLine)
        {
            Update(orderLine);
        }
    }
}
