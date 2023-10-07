using CashBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashBook.Services
{
    public interface IProductCategoryService : IGenericRepository<ProductCategory>
    {
        Task<ApiResult<SuccessResponseDto>> Create(ProductCategoryCreateDto request);
        Task<ApiResult<SuccessResponseDto>> Update(ProductCategoryUpdateDto request);
        Task<ApiResult<ProductCategoryDto>> Get(int id);
        Task<ApiResult<List<ProductCategoryDto>>> GetAll();
    }
}
