using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.BLL.BusinessModels
{
    public class CreatePaymentModel
    {
        public string? PaymentLink { get; set; }
        public OrderModel? Order { get; set; }
    }
}
