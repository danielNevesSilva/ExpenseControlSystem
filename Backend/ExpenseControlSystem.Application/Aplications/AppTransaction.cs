using AutoMapper;
using ExpenseControlSystem.Application.Dto;
using ExpenseControlSystem.Application.Exceptions;
using ExpenseControlSystem.Application.Interfaces;
using ExpenseControlSystem.Application.Validation;
using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Domain.Entities;
using ExpenseControlSystem.Domain.Interfaces.Service;

namespace ExpenseControlSystem.Application.Aplications
{
    public class AppTransaction : IAppTransaction
    {
        protected readonly IMapper _mapper;
        protected readonly IServiceTransaction _serviceTransaction;
        protected readonly IServicePerson _servicePerson;

        public AppTransaction(IServiceTransaction serviceTransaction,IServicePerson servicePerson ,IMapper mapper)
        {
            _mapper = mapper;
            _serviceTransaction = serviceTransaction;
            _servicePerson = servicePerson;

        }
        public async Task AddAsync(CreateTransactionDto entity)
        {
            var v = _mapper.Map<Transaction>(entity);
            await _serviceTransaction.AddAsync(v);
        }
        public async Task Execute(CreateTransactionDto entity)
        {
            await ValidateRequestCreate(entity);
            var v = _mapper.Map<Transaction>(entity);
            await _serviceTransaction.AddAsync(v);
        }

        public async Task DeleteAsync(TransactionViewModel entity)
        {
            var v = _mapper.Map<Transaction>(entity);
            await _serviceTransaction.DeleteAsync(v);
        }

        public async Task<IEnumerable<TransactionViewModel>> GetAllAsync()
        {
            var v = await _serviceTransaction.GetAllAsync();
            return _mapper.Map<List<TransactionViewModel>>(v);
        }

        public async Task<TransactionViewModel> GetByIdAsync(int id)
        {
            var v = await _serviceTransaction.GetByIdAsync(id);
            return _mapper.Map<TransactionViewModel>(v);
        }

        public async Task UpdateAsync(UpdateTransactionDto entity)
        {
            await ValidateRequestUpdate(entity);
            var v = _mapper.Map<Transaction>(entity);
            await _serviceTransaction.UpdateAsync(v);
        }
        public async Task<IEnumerable<TransactionViewModel>> GetByPersonIdAsync(int personId)
        {
            var v = await _serviceTransaction.GetByPersonIdAsync(personId);
            return _mapper.Map<List<TransactionViewModel>>(v);
        }

        public async Task<IEnumerable<TransactionViewModel>> GetByCategoryIdAsync(int categoryId)
        {
            var v = await _serviceTransaction.GetByCategoryIdAsync(categoryId);
            return _mapper.Map<List<TransactionViewModel>>(v);
        }

        public async Task<decimal> GetTotalIncomeByPersonAsync(int personId)
        {
            var v = await _serviceTransaction.GetTotalIncomeByPersonAsync(personId);
            return v;
        }

        public async Task<decimal> GetTotalExpenseByPersonAsync(int personId)
        {
            var v = await _serviceTransaction.GetTotalExpenseByPersonAsync(personId);
            return v;
        }

        public async Task ValidateRequestCreate(CreateTransactionDto model)
        {
            var v = new CreateTransactionValidation(_servicePerson);
            var result = await v.ValidateAsync(model);

            if (!result.IsValid)
            {
                var erroMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(erroMessages);
            }
        }
        public async Task ValidateRequestUpdate(UpdateTransactionDto model)
        {
            var v = new UpdateTransactionValidation(_servicePerson);
            var result = await v.ValidateAsync(model);

            if (!result.IsValid)
            {
                var erroMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ErrorOnValidationException(erroMessages);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }


    }
}
