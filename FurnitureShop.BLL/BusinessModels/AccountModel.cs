using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.BLL.BusinessModels
{
    public class AccountModel
    {
        public Guid Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public string? FullName {  get; set; }
        public string? PhoneNo {  get; set; }
        public string? Avatar { get; set; }
        public required string Status { get; set; } // gan bang status.ToString()
        public DateOnly? BirthDay { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}
