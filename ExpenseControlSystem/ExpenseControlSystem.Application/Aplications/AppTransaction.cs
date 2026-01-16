using AutoMapper;
using ExpenseControlSystem.Application.Interfaces;
using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Domain.Entities;
using ExpenseControlSystem.Domain.Interfaces.Service;

namespace ExpenseControlSystem.Application.Aplications
{
    public class AppTransaction : IAppTransaction
    {
        protected readonly IMapper _mapper;
        protected readonly IServiceTransaction _serviceTransaction;

        public AppTransaction(IServiceTransaction serviceTransaction, IMapper mapper)
        {
            _mapper = mapper;
            _serviceTransaction = serviceTransaction;

        }
        public async Task AddAsync(TransactionViewModel entity)
        {
           var v = _mapper.Map<Transaction>(entity);
            _serviceTransaction.AddAsync(v);
        }

        public async Task DeleteAsync(TransactionViewModel entity)
        {
            var v = _mapper.Map<Transaction>(entity);
            _serviceTransaction.DeleteAsync(v);
        }

        public async Task<IEnumerable<TransactionViewModel>> GetAllAsync()
        {
            var v = await _serviceTransaction.GetAllAsync();
            return _mapper.Map<List<TransactionViewModel>>(v);
        }

        public TransactionViewModel GetByIdAsync(int id)
        {
            var v = _serviceTransaction.GetByIdAsync(id);
            return _mapper.Map<TransactionViewModel>(v);
        }

        public async Task UpdateAsync(TransactionViewModel entity)
        {
            var v = _mapper.Map<Transaction>(entity);
            _serviceTransaction.UpdateAsync(v);
        }
        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
