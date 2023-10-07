using AutoMapper;
using CashBook.Data;

namespace CashBook.Services
{
    public class BankTransactionService : GenericRepository<BankTransaction>, IBankTransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BankTransactionService(CashBookDbContext context, IUnitOfWork unitOfWork, IMapper mapper) : base(context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResult<SuccessResponseDto>> Create(BankTransactionCreateRequestDto request)
        {
            var result = new ApiResult<SuccessResponseDto>();

            var entity = _mapper.Map<BankTransaction>(request);

            await _unitOfWork.BankTransactionRepository.AddAsync(entity);
           
            entity.CreateDate = DateTime.Now;
            var affectedRow = await _unitOfWork.Commit();

            result.ResultObject = new SuccessResponseDto
            {
                Message = affectedRow > 0 ? "Veri ekleme başarılı" : "Veri ekleme sırasında hata oluştu"
            };

            result.IsSucceed = affectedRow > 0;

            return result;
        }

        public async Task<ApiResult<BankTransactionGetDto>> Get(Guid id)
        {
            var result = new ApiResult<BankTransactionGetDto>();

            var bankTransaction = await _unitOfWork.BankTransactionRepository.FindByAsync(x => x.Id == id);

            if (bankTransaction == null)
            {
                result.IsSucceed = false;
                return result;
            }

            var dto = _mapper.Map<BankTransactionGetDto>(bankTransaction);
            result.ResultObject = dto;

            result.IsSucceed = true;
            return result;
        }

        public async Task<ApiResult<List<BankTransactionGetDto>>> GetAllByBankId(Guid bankId)
        {
            var result = new ApiResult<List<BankTransactionGetDto>>();

            var bankTransactions = await _unitOfWork.BankTransactionRepository.GetAllAsync(x => x.BankId == bankId);

            var list = _mapper.Map<List<BankTransactionGetDto>>(bankTransactions);

            result.ResultObject = new List<BankTransactionGetDto>();
            result.ResultObject = list;
            result.IsSucceed = true;
            return result;
        }

        public async Task<ApiResult<SuccessResponseDto>> Update(BankTransactionUpdateRequestDto request)
        {
            var result = new ApiResult<SuccessResponseDto>();
            var bankTransaction = await _unitOfWork.BankTransactionRepository.FindByAsync(x => x.Id == request.Id);

            if (bankTransaction == null)
            {
                result.IsSucceed = false;
                return result;
            }
            var entity = _mapper.Map<BankTransaction>(request);
            bankTransaction = entity;
            bankTransaction.ModifyDate = DateTime.Now;

             await _unitOfWork.BankTransactionRepository.UpdateAsync(bankTransaction);
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