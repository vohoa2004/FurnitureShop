using FurnitureShop.BLL.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderModel>> GetAllOrders();
        Task<(List<OrderModel>, int totalCount)> GetOrdersByUser(Guid accountId, int pageNo, int pageSize);
        Task<IEnumerable<OrderModel>> GetOrdersById(int id);
        Task CreateNewOrder(OrderModel order);
    }
}
