using CashBook.Services;
using Microsoft.AspNetCore.Mvc;

namespace CashBook.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseController : ControllerBase
    {
        IWarehouseService _warehouseService;
        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(WarehouseCreateDto requestDto)
        {
            var result = await _warehouseService.Create(requestDto);
            if (!result.IsSucceed)
            {
                return NotFound(result.Message);
            }
            return Ok(result.ResultObject);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update(WarehouseUpdateDto requestDto)
        {
            var result = await _warehouseService.Update(requestDto);
            if (!result.IsSucceed)
            {
                return NotFound(result.Message);
            }
            return Ok(result.ResultObject);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _warehouseService.Get(id);
            if (!result.IsSucceed)
            {
                return NotFound(result.Message);
            }
            return Ok(result.ResultObject);
        }
        [HttpGet("GetAllCompanyId")]
        public async Task<IActionResult> GetAllCompanyId(int id)
        {
            var result = await _warehouseService.GetAllCompanyId(id);
            if (!result.IsSucceed)
            {
                return NotFound(result.Message);
            }
            return Ok(result.ResultObject);
        }
    }
}