using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.BLL.BusinessModels
{
    public class RegisterModel
    {
        public required string Email { get; set; }

        public required string UserName { get; set; } 

        public required string Password { get; set; }
    }
}
