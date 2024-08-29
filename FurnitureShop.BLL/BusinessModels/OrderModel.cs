using FurnitureShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.BLL.BusinessModels
{
    public class OrderModel
    {
        public int? Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public double TotalPrice { get; set; }
        public Guid CustomerId { get; set; } // Khóa ngoại cho Customer
        public AccountModel? Customer { get; set; } // n-1

        public required List<OrderLineModel> OrderLines { get; set; }

        public required OrderStatus Status { get; set; }
        public required PaymentMethod PaymentMethod { get; set; }

        public string? TransactionId1 { get; set; } 
        
        // further business rule: bill > 20tr must deposit before deliver
        // => pay via bank account
        // => print QR with amount fixed = 20% total amount of the shop for customer to transfer money
        // and a form for customer to upload resulted transfer screen => store submitted form to a table
        // admin will check transfer screens and click button to update status
        public string? TransactionTime1 { get; set; }
        public double AmountTransaction1 { get; set; }
        public string? TransactionId2 { get; set; }
        public string? TransactionTime2 { get; set; }
        public double AmountTransaction2 { get; set; }

        public bool? IsDonePaid { get; set; }

    }
}
