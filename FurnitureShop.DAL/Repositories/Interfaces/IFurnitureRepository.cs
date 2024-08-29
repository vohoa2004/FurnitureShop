using FurnitureShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.DAL.Repositories.Interfaces
{
    public interface IFurnitureRepository
    {
        Task<IEnumerable<Furniture>> FindAllFurnituresAsync(bool trackChanges);
        Task<IEnumerable<Furniture>> FindFurnituresByCategory(int categoryId, bool trackChanges);
        Task<Furniture?> FindFunitureByIdAsync(int id, bool trackChanges);
        Task<(List<Furniture> Furnitures, int TotalCount)> FindFurnitureByFilter(
                                                            double? minPrice, 
                                                            double? maxPrice, 
                                                            string? name, 
                                                            List<int>? categoryIds, 
                                                            int pageNo, 
                                                            int pageSize
                                                          );
        void CreateFurniture(Furniture newFuniture);
        void UpdateFurniture(Furniture updatedFurniture);
        void DeleteFurniture(Furniture deletedFurniture);
        Task SaveFurniture();

    }
}
