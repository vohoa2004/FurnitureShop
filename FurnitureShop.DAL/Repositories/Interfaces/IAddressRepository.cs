using FurnitureShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.DAL.Repositories.Interfaces
{
    public interface IAddressRepository 
    {
        Task<List<Address>> FindAddressesByCustomer(Guid customer);
    }
}
