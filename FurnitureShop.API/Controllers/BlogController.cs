using FurnitureShop.API.DTOs.ResponseDTOs;
using FurnitureShop.BLL.BusinessModels;
using FurnitureShop.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpGet("Search")]
        public async Task<IActionResult> SearchBlog([FromQuery]SearchBlogModel model)
        {
            try
            {
                (List<BlogModel> searchList, int totalCount) = await _blogService.Search(model);
                var response = new PaginationDTO<BlogModel>(searchList, totalCount, model.PageNo, model.PageSize);
                return Ok(response);
            } catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            try
            {
                var blogModel = await _blogService.GetBlogById(id);
                if (blogModel == null)
                {
                    return NotFound("Blog Not Found!");
                }
                return Ok(blogModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
    }
}
