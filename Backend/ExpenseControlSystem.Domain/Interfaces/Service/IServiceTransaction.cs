using ExpenseControlSystem.Domain.Entities;

namespace ExpenseControlSystem.Domain.Interfaces.Service
{
    public interface IServiceTransaction : IServiceBase<Transaction>
    {
        Task<IEnumerable<Transaction>> GetByPersonIdAsync(int personId);
        Task<IEnumerable<Transaction>> GetByCategoryIdAsync(int categoryId);
        Task<decimal> GetTotalIncomeByPersonAsync(int personId);
        Task<decimal> GetTotalExpenseByPersonAsync(int personId);
    }
}
