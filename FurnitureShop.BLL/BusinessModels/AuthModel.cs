using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.BLL.BusinessModels
{
    public class AuthModel
    {
        public string AccessToken { get; set; } = "";

        public string RefreshToken { get; set; } = "";
    }
}
