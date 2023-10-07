using CashBook.Services;
using Microsoft.AspNetCore.Mvc;

namespace CashBook.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankTransactionController : ControllerBase
    {
        IBankTransactionService _bankTransactionService;
        public BankTransactionController(IBankTransactionService bankTransactionService)
        {
            _bankTransactionService = bankTransactionService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(BankTransactionCreateRequestDto requestDto)
        {
            var result = await _bankTransactionService.Create(requestDto);
            if (!result.IsSucceed)
            {
                return NotFound(result.Message);
            }
            return Ok(result.ResultObject);
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update(BankTransactionUpdateRequestDto requestDto)
        {
            var result = await _bankTransactionService.Update(requestDto);
            if (!result.IsSucceed)
            {
                return NotFound(result.Message);
            }
            return Ok(result.ResultObject);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _bankTransactionService.Get(id);
            if (!result.IsSucceed)
            {
                return NotFound(result.Message);
            }
            return Ok(result.ResultObject);
        }
        [HttpGet("GetAllByBankId")]
        public async Task<IActionResult> GetAllByBankId(Guid id)
        {
            var result = await _bankTransactionService.GetAllByBankId(id);
            if (!result.IsSucceed)
            {
                return NotFound(result.Message);
            }
            return Ok(result.ResultObject);
        }
    }
}