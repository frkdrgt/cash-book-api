using AutoMapper;
using CashBook.Data;

namespace CashBook.Services
{
    public class ProductService : GenericRepository<Product>, IProductService
    {
        IUnitOfWork _unitOfWork;
        IMapper _mapper;
        public ProductService(CashBookDbContext context, IUnitOfWork unitOfWork, IMapper mapper) : base(context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResult<SuccessResponseDto>> Create(ProductCreateDto request)
        {
            var result = new ApiResult<SuccessResponseDto>();

            var entity = _mapper.Map<Product>(request);

            await _unitOfWork.ProductRepository.AddAsync(entity);
         
            var affectedRow = await _unitOfWork.Commit();

            result.ResultObject = new SuccessResponseDto
            {
                Message = affectedRow > 0 ? "Veri ekleme başarılı" : "Veri ekleme sırasında hata oluştu"
            };

            result.IsSucceed = affectedRow > 0;

            return result;
        }

        public async Task<ApiResult<ProductGetDto>> Get(Guid id)
        {
            var result = new ApiResult<ProductGetDto>();

            var product = await _unitOfWork.ProductRepository.FindByAsync(x => x.Id == id);

            if (product == null)
            {
                result.IsSucceed = false;
                return result;
            }

            var dto = _mapper.Map<ProductGetDto>(product);
            result.ResultObject = dto;

            result.IsSucceed = true;
            return result;
        }

        public async Task<ApiResult<List<ProductGetDto>>> GetAllByCategoryId(int categoryId)
        {
            var result = new ApiResult<List<ProductGetDto>>();

            var bankTransactions = await _unitOfWork.ProductRepository.GetAllAsync(x => x.CategoryId == categoryId);

            var list = _mapper.Map<List<ProductGetDto>>(bankTransactions);

            result.ResultObject = new List<ProductGetDto>();
            result.ResultObject = list;
            result.IsSucceed = true;
            return result;
        }

        public async Task<ApiResult<SuccessResponseDto>> Update(ProductUpdateDto request)
        {
            var result = new ApiResult<SuccessResponseDto>();
            var product = await _unitOfWork.ProductRepository.FindByAsync(x => x.Id == request.Id);

            if (product == null)
            {
                result.IsSucceed = false;
                return result;
            }
            var entity = _mapper.Map<Product>(request);
            product = entity;

            await _unitOfWork.ProductRepository.UpdateAsync(product);
            var affectedRow = await _unitOfWork.Commit();

            result.ResultObject = new SuccessResponseDto
            {
                Message = affectedRow > 0 ? "Veri güncelleme işlemi başarılı" : "Veri güncelleme sırasında bir hata oluştu"
            };
            result.IsSucceed = affectedRow > 0;
            return result;
        }
    }
}