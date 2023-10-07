using AutoMapper;
using CashBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashBook.Services
{
    public class WarehouseService : GenericRepository<Warehouse>, IWarehouseService
    {
        IUnitOfWork _unitOfWork;
        IMapper _mapper;
        public WarehouseService(CashBookDbContext context, IUnitOfWork unitOfWork, IMapper mapper) : base(context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResult<SuccessResponseDto>> Create(WarehouseCreateDto request)
        {
            var result = new ApiResult<SuccessResponseDto>();

            var entity = _mapper.Map<Warehouse>(request);

            await _unitOfWork.WarehouseRepository.AddAsync(entity);
           
            var affectedRow = await _unitOfWork.Commit();

            result.ResultObject = new SuccessResponseDto
            {
                Message = affectedRow > 0 ? "Veri ekleme başarılı" : "Veri ekleme sırasında hata oluştu"
            };

            result.IsSucceed = affectedRow > 0;

            return result;
        }

        public async Task<ApiResult<WarehouseGetDto>> Get(Guid id)
        {
            var result = new ApiResult<WarehouseGetDto>();

            var warehouse = await _unitOfWork.WarehouseRepository.FindByAsync(x => x.Id == id);

            if (warehouse == null)
            {
                result.IsSucceed = false;
                return result;
            }

            var dto = _mapper.Map<WarehouseGetDto>(warehouse);
            result.ResultObject = dto;

            result.IsSucceed = true;
            return result;
        }

        public async Task<ApiResult<List<WarehouseGetDto>>> GetAllCompanyId(int companyId)
        {
            var result = new ApiResult<List<WarehouseGetDto>>();

            var warehouses = await _unitOfWork.WarehouseRepository.GetAllAsync(x => x.CompanyId == companyId);

            var list = _mapper.Map<List<WarehouseGetDto>>(warehouses);

            result.ResultObject = new List<WarehouseGetDto>();
            result.ResultObject = list;
            result.IsSucceed = true;
            return result;
        }

        public async Task<ApiResult<SuccessResponseDto>> Update(WarehouseUpdateDto request)
        {
            var result = new ApiResult<SuccessResponseDto>();
            var warehouse = await _unitOfWork.WarehouseRepository.FindByAsync(x => x.Id == request.Id);

            if (warehouse == null)
            {
                result.IsSucceed = false;
                return result;
            }
            var entity = _mapper.Map<Warehouse>(request);
            warehouse = entity;

            await _unitOfWork.WarehouseRepository.UpdateAsync(warehouse);
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