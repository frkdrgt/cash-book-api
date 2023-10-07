using CashBook.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashBook.Services
{
    public class UserService : GenericRepository<User>, IUserService
    {
        public IUnitOfWork _unitOfWork;
        public IConfiguration _configuration;
        public UserService(CashBookDbContext context, IUnitOfWork unitOfWork, IConfiguration configuration) : base(context)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<ApiResult<AuthResponseDto>> Login(UserLoginRequestDto requestDto)
        {
            var result = new ApiResult<AuthResponseDto>();

            var user = await _unitOfWork.UserRepository.FindByAsync(x => x.UserName.ToLower().Equals(requestDto.UserName.ToLower()) && x.Password.ToLower().Equals(requestDto.PasswordHash) && x.IsActive);
            if (user == null)
            {
                result.IsSucceed = false;
                return result;
            }

            //var passwordHash = BCrypt.Net.BCrypt.Verify(requestDto.PasswordHash, user.HashKey);

            //if (!passwordHash)
            //{
            //    result.IsSucceed = false;
            //    return result;
            //}

            var secretKey = _configuration["JwtToken:SecretKey"];
            var issuer = _configuration["JwtToken:Issuer"];
            string token = JWTTokenManager.CreateToken(user.UserName, secretKey, issuer);

            result.IsSucceed = true;
            result.ResultObject = new AuthResponseDto
            {
                Id = user.Id,
                Token = token,
                Name = user.FirstName + " " + user.LastName,
                CompanyId = user.CompanyId
            };

            return result;
        }

        public Task<ApiResult<SuccessResponseDto>> Register(UserRegisterRequestDto requestDto)
        {
            throw new NotImplementedException();
        }
    }
}
