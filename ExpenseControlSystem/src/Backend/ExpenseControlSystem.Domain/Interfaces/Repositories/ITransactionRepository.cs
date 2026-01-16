using ExpenseControlSystem.Domain.Entities;

namespace ExpenseControlSystem.Domain.Interfaces.Repositories
{
    public interface ITransactionRepository : IRepositoryBase<Transaction>
    {
        Task<IEnumerable<Transaction>> GetByPersonIdAsync(int personId);
        Task<IEnumerable<Transaction>> GetByCategoryIdAsync(int categoryId);
        Task<decimal> GetTotalIncomeByPersonAsync(int personId);
        Task<decimal> GetTotalExpenseByPersonAsync(int personId);
    }
}
