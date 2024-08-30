using FurnitureShop.BLL.BusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.BLL.Services.Interfaces
{
    public interface IBlogService
    {
        Task<(List<BlogModel> blogs, int totalCount)> Search(SearchBlogModel searchBlogModel);
        Task<BlogModel?> GetBlogById(int id);
    }
}
