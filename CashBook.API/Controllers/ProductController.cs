using CashBook.Services;
using Microsoft.AspNetCore.Mvc;

namespace CashBook.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(ProductCreateDto requestDto)
        {
            var result = await _productService.Create(requestDto);
            if (!result.IsSucceed)
            {
                return NotFound(result.Message);
            }
            return Ok(result.ResultObject);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(ProductUpdateDto requestDto)
        {
            var result = await _productService.Update(requestDto);
            if (!result.IsSucceed)
            {
                return NotFound(result.Message);
            }
            return Ok(result.ResultObject);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _productService.Get(id);
            if (!result.IsSucceed)
            {
                return NotFound(result.Message);
            }
            return Ok(result.ResultObject);
        }
        [HttpGet("GetAllByCategoryId")]
        public async Task<IActionResult> GetAllByCategoryId(int id)
        {
            var result = await _productService.GetAllByCategoryId(id);
            if (!result.IsSucceed)
            {
                return NotFound(result.Message);
            }
            return Ok(result.ResultObject);
        }
    }
}
