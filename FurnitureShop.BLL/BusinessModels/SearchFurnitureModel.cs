using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.BLL.BusinessModels
{
    public class SearchFurnitureModel
    {
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public string? Name { get; set; }
        public List<int>? ListCategoryIds { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
}
