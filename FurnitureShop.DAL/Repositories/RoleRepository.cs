using FurnitureShop.DAL.Entities;
using FurnitureShop.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.DAL.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(FurnitureShopContext context) : base(context)
        {
        }

        public async Task<Role?> GetById(int id)
        {
            return await FindByCondition(r => r.Id.Equals(id), false).SingleOrDefaultAsync();
        }
    }
}
