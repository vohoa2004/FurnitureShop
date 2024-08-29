using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.DAL.Entities
{
    public class OrderLine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double LinePrice { get; set;}
        public int OrderId { get; set; } // Khóa ngoại cho Order
        public int ProductId { get; set; } // Khóa ngoại cho Furniture

        public double ProductPrice { get; set; }

        [ForeignKey("OrderId")]
        public virtual required Order Order { get; set; } // n - 1

        [ForeignKey("ProductId")]
        public virtual required Furniture Product { get; set; } // n - 1

    }
}
