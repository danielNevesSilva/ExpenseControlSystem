using ExpenseControlSystem.Domain.Entities;
using ExpenseControlSystem.Domain.Interfaces.IoC;
using ExpenseControlSystem.Domain.Interfaces.Repositories;
using ExpenseControlSystem.Domain.Interfaces.Service;

namespace ExpenseControlSystem.Domain.Services
{
    public class ServiceTransaction : ServiceBase<Transaction>, IServiceTransaction
    {
        private readonly ITransactionRepository _repository;

        public ServiceTransaction(ITransactionRepository Transactionrepository, IUnitOfWork unitOfWork) : base(Transactionrepository, unitOfWork)
        {
            _repository = Transactionrepository;

        }

        public async Task<IEnumerable<Transaction>> GetByCategoryIdAsync(int categoryId)
        {
            return await _repository.GetByCategoryIdAsync(categoryId);
        }

        public async Task<IEnumerable<Transaction>> GetByPersonIdAsync(int personId)
        {
            return await _repository.GetByPersonIdAsync(personId);
        }

        public async Task<decimal> GetTotalExpenseByPersonAsync(int personId)
        {
            return await _repository.GetTotalExpenseByPersonAsync(personId);
        }

        public async Task<decimal> GetTotalIncomeByPersonAsync(int personId)
        {
            return await _repository.GetTotalIncomeByPersonAsync((int)personId);
        }
    }
}
