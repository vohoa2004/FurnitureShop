using FurnitureShop.BLL.BusinessModels;
using FurnitureShop.DAL.Entities;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.BLL.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModel> Register(RegisterModel registerModel);
        Task<AuthModel> LoginWithUsernameAndPassword(string username, string password);

    }
}
