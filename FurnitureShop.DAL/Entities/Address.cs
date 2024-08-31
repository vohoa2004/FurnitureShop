using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.DAL.Entities
{
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string HouseNumber { get; set; }
        public required string Street { get; set; }
        public required string District { get; set; }
        public required string ProvinceOrCity {  get; set; }
        public required string Country { get; set; }
        public required string PostalCode { get; set; }
        public Guid CustomerId { get; set; } // Khóa ngoại cho Customer

        [ForeignKey("CustomerId")]
        public virtual required Account Customer { get; set; } // n-1
    }
}
