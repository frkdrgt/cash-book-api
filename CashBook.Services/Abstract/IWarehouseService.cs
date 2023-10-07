using CashBook.Data;

namespace CashBook.Services
{
    public interface IWarehouseService : IGenericRepository<Warehouse>
    {
        Task<ApiResult<SuccessResponseDto>> Create(WarehouseCreateDto request);
        Task<ApiResult<SuccessResponseDto>> Update(WarehouseUpdateDto request);
        Task<ApiResult<WarehouseGetDto>> Get(Guid id);
        Task<ApiResult<List<WarehouseGetDto>>> GetAllCompanyId(int companyId);
    }
}