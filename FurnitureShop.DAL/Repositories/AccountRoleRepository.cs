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
    public class AccountRoleRepository : GenericRepository<AccountRole>, IAccountRoleRepository
    {
        public AccountRoleRepository(FurnitureShopContext context) : base(context)
        {
        }

        public async Task<IEnumerable<AccountRole>> GetByAccountId(Guid id)
        {
            return await FindByCondition(ar => ar.AccountId.Equals(id), false).ToListAsync();
        }
    }
}
