using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.DAL.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public double TotalPrice { get; set; }
        public Guid CustomerId { get; set; } // Khóa ngoại cho Customer

        [ForeignKey("CustomerId")]
        public virtual required Account Customer { get; set; } // n-1

        public virtual ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();

        public required OrderStatus Status { get; set; }
        public required PaymentMethod PaymentMethod { get; set; }
        public string? TransactionId1 { get; set; }
        public string? TransactionTime1 { get; set; }
        public double AmountTransaction1 { get; set; }
        public string? TransactionId2 { get; set; }
        public string? TransactionTime2 { get; set; }
        public double AmountTransaction2 { get; set; }
        public bool IsDonePaid { get; set; } 
    }
    public enum OrderStatus
    {
        Processing_From_Warehouse,
        Delivering,
        Done,
        Canceled,
        Refunding,
        Refunded
    }
    public enum PaymentMethod
    {
        Offline,
        VNPay,
        MOMO,
        Paypal
    }
}
