using AutoMapper;
using CashBook.Data;

namespace CashBook.Services
{
    public class ProductCategoryService : GenericRepository<ProductCategory>, IProductCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductCategoryService(CashBookDbContext context, IUnitOfWork unitOfWork, IMapper mapper) : base(context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResult<SuccessResponseDto>> Create(ProductCategoryCreateDto request)
        {
            var result = new ApiResult<SuccessResponseDto>();

            var entity = _mapper.Map<ProductCategory>(request);

            await _unitOfWork.ProductCategoryRepository.AddAsync(entity);
            var affectedRow = await _unitOfWork.Commit();

            result.ResultObject = new SuccessResponseDto
            {
                Message = affectedRow > 0 ? "Veri ekleme başarılı" : "Veri ekleme sırasında hata oluştu"
            };

            result.IsSucceed = affectedRow > 0;

            return result;
        }

        public async Task<ApiResult<ProductCategoryDto>> Get(int id)
        {
            var result = new ApiResult<ProductCategoryDto>();

            var productCategory = await _unitOfWork.ProductCategoryRepository.FindByAsync(x => x.Id == id);

            if (productCategory == null)
            {
                result.IsSucceed = false;
                return result;
            }

            var dto = _mapper.Map<ProductCategoryDto>(productCategory);
            result.ResultObject = dto;

            result.IsSucceed = true;
            return result;
        }

        public async Task<ApiResult<List<ProductCategoryDto>>> GetAll()
        {
            var result = new ApiResult<List<ProductCategoryDto>>();

            var productCategories = await _unitOfWork.ProductCategoryRepository.GetAllAsync();

            var list = _mapper.Map<List<ProductCategoryDto>>(productCategories);

            result.ResultObject = new List<ProductCategoryDto>();
            result.ResultObject = list;
            result.IsSucceed = true;
            return result;
        }

        public async Task<ApiResult<SuccessResponseDto>> Update(ProductCategoryUpdateDto request)
        {
            var result = new ApiResult<SuccessResponseDto>();
            var productCategory = await _unitOfWork.ProductCategoryRepository.FindByAsync(x => x.Id == request.Id);

            if (productCategory == null)
            {
                result.IsSucceed = false;
                return result;
            }
            var entity = _mapper.Map<ProductCategory>(request);
            productCategory = entity;

            await _unitOfWork.ProductCategoryRepository.UpdateAsync(productCategory);
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
