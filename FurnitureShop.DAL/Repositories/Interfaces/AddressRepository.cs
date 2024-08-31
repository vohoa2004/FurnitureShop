using FurnitureShop.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.DAL.Repositories.Interfaces
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(FurnitureShopContext context) : base(context)
        {
        }

        public async Task<List<Address>> FindAddressesByCustomer(Guid customerId)
        {
            return await FindByCondition(a => a.CustomerId == customerId, false).ToListAsync();
        }
    }
}
