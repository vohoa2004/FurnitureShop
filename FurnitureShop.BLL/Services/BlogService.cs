using AutoMapper;
using FurnitureShop.BLL.BusinessModels;
using FurnitureShop.BLL.Services.Interfaces;
using FurnitureShop.DAL.Entities;
using FurnitureShop.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.BLL.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;
        public BlogService(IBlogRepository blogRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
        }

        public async Task<BlogModel?> GetBlogById(int id)
        {
            var blog = await _blogRepository.FindBlogByIdAsync(id, false);

            if (blog != null)
            {
                BlogModel result = _mapper.Map<BlogModel>(blog);
                return result;
            }
            return null;
        }

        public async Task<(List<BlogModel> blogs, int totalCount)> Search(SearchBlogModel searchBlogModel)
        {
            (List<Blog> blogs, int totalCount) = await _blogRepository.FindBlogByFilter(
                                                                        searchBlogModel.CreatedAt,
                                                                        searchBlogModel.Title,
                                                                        searchBlogModel.PageNo,
                                                                        searchBlogModel.PageSize);
            List<BlogModel> blogReponseList = _mapper.Map<List<BlogModel>>(blogs);
            return (blogReponseList, totalCount);
        }
    }
}
