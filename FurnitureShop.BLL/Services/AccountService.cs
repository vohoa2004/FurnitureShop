using AutoMapper;
using FurnitureShop.BLL.BusinessModels;
using FurnitureShop.BLL.Services.Interfaces;
using FurnitureShop.DAL.Entities;
using FurnitureShop.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public AccountService(IAddressRepository addressRepository, IMapper mapper, IAccountRepository accountRepository) 
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
            _accountRepository = accountRepository;
        }

        public async Task<AccountModel?> GetAccountById(Guid id)
        {
            Account? account = await _accountRepository.FindById(id);
            if (account == null)
            {
                throw new KeyNotFoundException("Account not found!");
            }
            return _mapper.Map<AccountModel>(account);
        }

        public async Task<List<AddressModel>> GetAddressesByCustomer(Guid customerId)
        {
            List<Address> addresses = await _addressRepository.FindAddressesByCustomer(customerId);
            List<AddressModel> result = _mapper.Map<List<AddressModel>>(addresses); // map data to a new instance => for create operation
            return result;
        }

        public async Task<AccountModel> UpdateAccount(Guid accountId, UpdateAccountModel accountDto)
        {
            Account? account = await _accountRepository.FindById(accountId);
            if (account == null)
            {
                throw new KeyNotFoundException("Account not found!");
            }

            _mapper.Map(accountDto, account); // map data from accountDto to account => map data from instance A to instance B => for update operation

            _accountRepository.UpdateAccount(account);

            await _accountRepository.SaveAccount();

            return _mapper.Map<AccountModel>(account);
        }
    }
}
