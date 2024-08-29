using FurnitureShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.DAL.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account?> FindById(Guid id);
        Task<Account?> FindByUsernameAndPassword(string username, string password);
        Task<Account?> FindByUsername(string username);
        Task<Account?> FindByEmail(string email);
        Task<Account?> FindByPhoneNo(string phoneNo);
        void CreateAccount(Account newAccount);
        void UpdateAccount(Account updatedAccount);
        void DeleteAccount(Account deletedAccount);
        Task SaveAccount();
    }
}
