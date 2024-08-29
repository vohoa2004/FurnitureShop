using FurnitureShop.DAL.Entities;
using FurnitureShop.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.DAL.Repositories
{
    public class FurnitureRepository : GenericRepository<Furniture>, IFurnitureRepository
    {

        // get all furnitures
        // mai paging sau
        public FurnitureRepository(FurnitureShopContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Furniture>> FindAllFurnituresAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).ToListAsync();
        }

        public async Task<IEnumerable<Furniture>> FindFurnituresByCategory(int categoryId, bool trackChanges)
        {
            return await FindByCondition(f => f.CategoryId.Equals(categoryId), trackChanges).ToListAsync();
        }

        public async Task<Furniture?> FindFunitureByIdAsync(int id, bool trackChanges)
        {
            return await FindByCondition(f => f.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
        }

        public void CreateFurniture(Furniture n) => Create(n);
        public void UpdateFurniture(Furniture u) => Update(u);
        public void DeleteFurniture(Furniture d) => Delete(d);

        public async Task SaveFurniture() => await Save();

        // return a tuple with 2 values
        public async Task<(List<Furniture> Furnitures, int TotalCount)> FindFurnitureByFilter(double? minPrice, double? maxPrice, string? name, List<int>? categoryIds, int pageNo, int pageSize)
        {
            var query = _context.Set<Furniture>().AsQueryable();
            query = query.Where(f => (minPrice == null || minPrice <= f.Price)
                                  && (maxPrice == null || maxPrice >= f.Price)
                                  && (name == null || f.Name.ToLower().Contains(name.ToLower()))
                                  && (categoryIds == null || categoryIds.Count == 0 || categoryIds.Contains(f.CategoryId)));
            var totalCount = await query.CountAsync();
            query = query.Skip((pageNo - 1) * pageSize).Take(pageSize); // offset n-1 trang truoc, limit 8 phan tu de lay trang thu n
            var furnitureList = await query.ToListAsync();
            return (furnitureList, totalCount);
        }
    }
}
