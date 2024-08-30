using AutoMapper;
using FurnitureShop.API.DTOs.RequestDTOs;
using FurnitureShop.API.DTOs.ResponseDTOs;
using FurnitureShop.BLL.BusinessModels;
using FurnitureShop.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FurnitureShop.API.Controllers
{
    // localhost:xxxx/api/furnitures
    [Route("api/[controller]")]
    [ApiController]
    public class FurnitureController : ControllerBase
    {
        private readonly IFurnitureService _furnitureService;
        private readonly IMapper _mapper;


        public FurnitureController(IFurnitureService furnitureService, IMapper mapper)
        {
            _furnitureService = furnitureService;
            _mapper = mapper;
        }
        // CRUD furnitures

        // 1. get list of all furniture
        [HttpGet]
        public async Task<IActionResult> GetAllFurniture() // mai phan trang
                                                           // async await cho nhanh len
        {
            return Ok(await _furnitureService.GetAllFurnitures());

        }

        // 2. get furniture by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFurnitureById(int id)
        {
            var furniture = await _furnitureService.GetById(id);
            if (furniture == null)
            {
                return NotFound("Furniture Not Found!");
            }
            return Ok(furniture);
        }

        // 3. add a new furniture
        [HttpPost]
        public async Task<IActionResult> CreateFurniture([FromBody] FurnitureRequestDTO dto)
        {
            var model = _mapper.Map<FurnitureModel>(dto);
            return Ok(await _furnitureService.AddNewFurniture(model));
        }

        // 4. update a furniture
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFurniture(int id, [FromBody] FurnitureRequestDTO dto)
        {
            try
            {
                var model = _mapper.Map<FurnitureModel>(dto);
                return Ok(await _furnitureService.UpdateFurniture(id, model));
            }
            catch (KeyNotFoundException ne)
            {
                return NotFound(ne.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // 5. delete a furniture
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFurniture(int id)
        {
            try
            {
                await _furnitureService.DeleteFurniture(id);
                return NoContent();
            }
            catch (KeyNotFoundException ne)
            {
                return NotFound(ne.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // 6. Tìm theo tên sp, giá trong vùng min, max price, category thuộc 1 trong các category đc đưa vào dto
        // phân trang 8 sản phẩm trả về
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchFurnitureRequestDTO dto)
        {
            var categoryIdsList = dto.GetCategoryIdsList();
            SearchFurnitureModel searchFurnitureModel = _mapper.Map<SearchFurnitureModel>(dto);
            searchFurnitureModel.ListCategoryIds = categoryIdsList;
            var (searchResult,totalCount) = await _furnitureService.Search(searchFurnitureModel);
            var responseData = new PaginationDTO<FurnitureModel>(searchResult, 
                                                                totalCount, 
                                                                searchFurnitureModel.PageNo, 
                                                                searchFurnitureModel.PageSize);

            return Ok(responseData);
        }

    }
}
