using CashBook.Data;

namespace CashBook.Services
{
    public interface IProductService : IGenericRepository<Product>
    {
        Task<ApiResult<SuccessResponseDto>> Create(ProductCreateDto request);
        Task<ApiResult<SuccessResponseDto>> Update(ProductUpdateDto request);
        Task<ApiResult<ProductGetDto>> Get(Guid id);
        Task<ApiResult<List<ProductGetDto>>> GetAllByCategoryId(int categoryId);
    }
}