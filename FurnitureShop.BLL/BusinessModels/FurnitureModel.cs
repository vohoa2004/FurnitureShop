using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.BLL.BusinessModels
{
    public class FurnitureModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int AvailableQuantity { get; set; }
        public string? ImageLink { get; set; }
        public bool IsShown { get; set; }
        public int CategoryId { get; set; }
    }
}
