using AutoMapper;
using FurnitureShop.BLL.BusinessModels;
using FurnitureShop.BLL.Services.Interfaces;
using FurnitureShop.DAL.Entities;
using FurnitureShop.DAL.Repositories;
using FurnitureShop.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.BLL.Services
{
    public class FurnitureService : IFurnitureService
    {
        private readonly IFurnitureRepository _furnitureRepo;

        private readonly IMapper _mapper;

        public FurnitureService(IFurnitureRepository furnitureRepo, IMapper mapper)
        {
            _furnitureRepo = furnitureRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FurnitureModel>> GetAllFurnitures()
        {
            var furnitures = await _furnitureRepo.FindAllFurnituresAsync(false);
            var furnitureModels = _mapper.Map<IEnumerable<FurnitureModel>>(furnitures);
            return furnitureModels;
        }

        public async Task<FurnitureModel?> GetById(int id)
        {
            var found = await _furnitureRepo.FindFunitureByIdAsync(id, false);
            FurnitureModel? result = null;
            if (found != null)
            {
                result = _mapper.Map<FurnitureModel>(found);
            }
            return result;

        }

        public async Task<FurnitureModel> AddNewFurniture(FurnitureModel model)
        {
            var furniture = _mapper.Map<Furniture>(model);
            
            _furnitureRepo.CreateFurniture(furniture);
            await _furnitureRepo.SaveFurniture();
            
            return model;
        }

        
        public async Task DeleteFurniture(int id)
        {

            var furniture = await _furnitureRepo.FindFunitureByIdAsync(id, false);
            if (furniture == null)
            {
                throw new KeyNotFoundException("Furniture Not Found");
            }

            _furnitureRepo.DeleteFurniture(furniture);
            await _furnitureRepo.SaveFurniture();
        }

        public async Task<FurnitureModel?> UpdateFurniture(int id, FurnitureModel furniture)
        {
            var found = await _furnitureRepo.FindFunitureByIdAsync(id, true);
            if (found == null)
            {
                throw new KeyNotFoundException($"Furniture with ID {id} not found.");
            }

            _mapper.Map(furniture, found);

            await _furnitureRepo.SaveFurniture();

            return _mapper.Map<FurnitureModel>(found);
        }

        public async Task<(List<FurnitureModel>, int totalCount)> Search(SearchFurnitureModel model)
        {
            var (listDB, totalCount) = await _furnitureRepo.FindFurnitureByFilter(
                model.MinPrice, 
                model.MaxPrice, 
                model.Name, 
                model.ListCategoryIds, 
                model.PageNo, 
                model.PageSize);
            List<FurnitureModel> furnitureModels = _mapper.Map<List<FurnitureModel>>(listDB);
            return (furnitureModels, totalCount);

        }

    }
}
