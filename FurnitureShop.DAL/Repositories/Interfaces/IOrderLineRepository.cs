using FurnitureShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.DAL.Repositories.Interfaces
{
    public interface IOrderLineRepository 
    {
        Task<IEnumerable<OrderLine>> GetOrderLinesByOrderId(int orderId);

        void CreateOrderLine(OrderLine orderLine);

        void DeleteOrderLine(OrderLine orderLine);

        Task SaveOrderLine();

        void UpdateOrderLine(OrderLine orderLine);
    }
}
