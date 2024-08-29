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
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(FurnitureShopContext context) : base(context)
        {
        }
        public async Task<Account?> FindByEmail(string email)
        {
            return await FindByCondition(a => a.Email.Equals(email), false).FirstOrDefaultAsync();
        }

        public async Task<Account?> FindByPhoneNo(string phoneNo)
        {
            return await FindByCondition(a => phoneNo.Equals(a.PhoneNo), false).FirstOrDefaultAsync();
        }

        public async Task<Account?> FindByUsername(string username)
        {
            return await FindByCondition(a => a.UserName.Equals(username), false).FirstOrDefaultAsync();
        }

        public async Task<Account?> FindByUsernameAndPassword(string username, string password)
        {
            return await FindByCondition(a => a.UserName.Equals(username) && a.Password.Equals(password), false).FirstOrDefaultAsync();
        }

        public async Task SaveAccount()
        {
            await Save();
        }

        public void CreateAccount(Account newAccount)
        {
            Create(newAccount);
        }

        public void DeleteAccount(Account deletedAccount)
        {
            Delete(deletedAccount);
        }

        public void UpdateAccount(Account updatedAccount)
        {
            Update(updatedAccount);
        }

        public async Task<Account?> FindById(Guid id)
        {
            return await FindByCondition(a => a.Id == id, false).FirstOrDefaultAsync();
        }
    }
}
