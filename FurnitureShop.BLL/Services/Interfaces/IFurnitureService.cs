using FurnitureShop.BLL.BusinessModels;
using FurnitureShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.BLL.Services.Interfaces
{
    public interface IFurnitureService
    {
        Task<IEnumerable<FurnitureModel>> GetAllFurnitures();
        Task<FurnitureModel?> GetById(int id);
        Task<(List<FurnitureModel>, int totalCount)> Search(SearchFurnitureModel model);
        Task<FurnitureModel> AddNewFurniture(FurnitureModel furniture);
        Task<FurnitureModel?> UpdateFurniture(int id,  FurnitureModel furniture);
        Task DeleteFurniture(int id);
    }
}
