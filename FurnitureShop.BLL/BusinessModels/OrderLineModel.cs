using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.BLL.BusinessModels
{
    public class OrderLineModel
    {
        public  required int ProductId { get; set; }
        public FurnitureModel? Product { get; set; }
        public double ProductPrice { get; set; }
        public int? OrderId { get; set; }
        public int Quantity { get; set; }
        public double LinePrice { get; set; }


    }
}
