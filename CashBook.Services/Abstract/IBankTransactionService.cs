using CashBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashBook.Services
{
    public interface IBankTransactionService : IGenericRepository<BankTransaction>
    {
        Task<ApiResult<SuccessResponseDto>> Create(BankTransactionCreateRequestDto request);
        Task<ApiResult<SuccessResponseDto>> Update(BankTransactionUpdateRequestDto request);
        Task<ApiResult<BankTransactionGetDto>> Get(Guid id);
        Task<ApiResult<List<BankTransactionGetDto>>> GetAllByBankId(Guid bankId);
    }
}