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
    }
}
