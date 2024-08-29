using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.BLL.BusinessModels
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string ImageLink { get; set; }
    }
}
