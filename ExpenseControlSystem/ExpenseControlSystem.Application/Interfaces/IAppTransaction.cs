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
        TransactionViewModel GetByIdAsync(int id);
        Task<IEnumerable<TransactionViewModel>> GetAllAsync();
        Task AddAsync(TransactionViewModel entity);
        Task UpdateAsync(TransactionViewModel entity);
        Task DeleteAsync(TransactionViewModel entity);
    }
}
