using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.BLL.BusinessModels
{
    public class AddressModel
    {
        public int Id { get; set; }
        public required string HouseNumber { get; set; }
        public required string Street { get; set; }
        public required string District { get; set; }
        public required string ProvinceOrCity { get; set; }
        public required string Country { get; set; }
        public required string PostalCode { get; set; }
        public Guid CustomerId { get; set; } // Khóa ngoại cho Customer
    }
}
