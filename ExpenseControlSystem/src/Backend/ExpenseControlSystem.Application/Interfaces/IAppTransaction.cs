using ExpenseControlSystem.Application.Dto;
using ExpenseControlSystem.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControlSystem.Application.Interfaces
{
    public interface IAppTransaction : IDisposable
    {
        Task<TransactionViewModel> GetByIdAsync(int id);
        Task<IEnumerable<TransactionViewModel>> GetAllAsync();
        Task AddAsync(CreateTransactionDto entity);
        Task Execute(CreateTransactionDto entity);
        Task UpdateAsync(UpdateTransactionDto entity);
        Task DeleteAsync(TransactionViewModel entity);
        Task<IEnumerable<TransactionViewModel>> GetByPersonIdAsync(int personId);
        Task<IEnumerable<TransactionViewModel>> GetByCategoryIdAsync(int categoryId);
        Task<decimal> GetTotalIncomeByPersonAsync(int personId);
        Task<decimal> GetTotalExpenseByPersonAsync(int personId);
    }
}
