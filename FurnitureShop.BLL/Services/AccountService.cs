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
        private readonly IMapper _mapper;

        public AccountService(IAddressRepository addressRepository, IMapper mapper) 
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<List<AddressModel>> GetAddressesByCustomer(Guid customerId)
        {
            List<Address> addresses = await _addressRepository.FindAddressesByCustomer(customerId);
            List<AddressModel> result = _mapper.Map<List<AddressModel>>(addresses);
            return result;
        }

    }
}
