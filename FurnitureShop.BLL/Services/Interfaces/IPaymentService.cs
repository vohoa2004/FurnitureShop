using FurnitureShop.BLL.BusinessModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.BLL.Services.Interfaces
{
    public interface IPaymentService
    {
        string CreatePayment(OrderModel orderModel, HttpContext context); 
        Task<bool> ReturnFromVNPay(VNPayModel vnPayModel);
        Task ProcessOrder(OrderModel orderDto, VNPayModel vnPayModel);
    }
}
