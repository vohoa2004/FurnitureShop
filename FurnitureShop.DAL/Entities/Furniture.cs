using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.DAL.Entities
{
    public class Furniture
    {
        [Key]
        [DatabaseGenerated (DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int AvailableQuantity { get; set; }
        public string? ImageLink { get; set; }
        public bool IsShown { get; set; }
        public int CategoryId { get; set; } // Khóa ngoại cho Category

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; } // n - 1

    }
}
