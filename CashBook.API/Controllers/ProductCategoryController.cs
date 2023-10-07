using CashBook.Services;
using Microsoft.AspNetCore.Mvc;

namespace CashBook.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductCategoryController : ControllerBase
    {
        IProductCategoryService _productCategoryService;
        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(ProductCategoryCreateDto requestDto)
        {
            var result = await _productCategoryService.Create(requestDto);
            if (!result.IsSucceed)
            {
                return NotFound(result.Message);
            }
            return Ok(result.ResultObject);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(ProductCategoryUpdateDto requestDto)
        {
            var result = await _productCategoryService.Update(requestDto);
            if (!result.IsSucceed)
            {
                return NotFound(result.Message);
            }
            return Ok(result.ResultObject);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _productCategoryService.Get(id);
            if (!result.IsSucceed)
            {
                return NotFound(result.Message);
            }
            return Ok(result.ResultObject);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productCategoryService.GetAll();
            if (!result.IsSucceed)
            {
                return NotFound(result.Message);
            }
            return Ok(result.ResultObject);
        }
    }
}