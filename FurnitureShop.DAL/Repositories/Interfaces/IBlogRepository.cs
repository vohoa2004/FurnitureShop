using FurnitureShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.DAL.Repositories.Interfaces
{
    public interface IBlogRepository
    {
        Task<IEnumerable<Blog>> FindAllBlogsAsync(bool trackChanges);
        Task<IEnumerable<Blog>> FindBlogsByCategory(int categoryId, bool trackChanges);
        Task<Blog?> FindBlogByIdAsync(int id, bool trackChanges);
        Task<(List<Blog> Blogs, int TotalCount)> FindBlogByFilter(
                                                            DateTime? createdAt,
                                                            string? title,
                                                            int pageNo,
                                                            int pageSize
                                                          );
        void CreateBlog(Blog newBlog);
        void UpdateBlog(Blog updatedBlog);
        void DeleteBlog(Blog deletedBlog);
        Task SaveBlog();
    }
}
