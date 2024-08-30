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
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        public BlogRepository(FurnitureShopContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Blog>> FindAllBlogsAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).ToListAsync();
        }

        public async Task<(List<Blog> Blogs, int TotalCount)> FindBlogByFilter(DateTime? createdAt, 
            string? title, int pageNo, int pageSize)
        {
            var query = _context.Set<Blog>().AsQueryable();
            query = query.Where(b => (createdAt == null || b.CreatedAt.Date == createdAt.Value.Date)
                                    && (title == null || b.Title.ToLower().Contains(title.ToLower()))
                                    )
                         .OrderByDescending(b => b.Id);
            var totalCount = await query.CountAsync();
            query = query.Skip((pageNo - 1) * pageSize).Take(pageSize);
            var list = await query.ToListAsync();
            return (list, totalCount);
        }

        public async Task<Blog?> FindBlogByIdAsync(int id, bool trackChanges)
        {
            return await FindByCondition(b => b.Id == id, trackChanges).FirstOrDefaultAsync();
        }

        public Task<IEnumerable<Blog>> FindBlogsByCategory(int categoryId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void CreateBlog(Blog newBlog)
        {
            throw new NotImplementedException();
        }

        public void DeleteBlog(Blog deletedBlog)
        {
            throw new NotImplementedException();
        }

        public Task SaveBlog()
        {
            throw new NotImplementedException();
        }

        public void UpdateBlog(Blog updatedBlog)
        {
            throw new NotImplementedException();
        }
    }
}
