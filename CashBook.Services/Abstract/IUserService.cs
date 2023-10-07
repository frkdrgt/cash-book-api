using CashBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashBook.Services
{
    public interface IUserService : IGenericRepository<User>
    {
        Task<ApiResult<SuccessResponseDto>> Register(UserRegisterRequestDto requestDto);
        Task<ApiResult<AuthResponseDto>> Login(UserLoginRequestDto requestDto);
    }
}