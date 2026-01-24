using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Domain.Entities;

namespace ExpenseControlSystem.Application.Interfaces
{
    public interface IAppPerson : IDisposable
    {
        Task<PersonViewModel> GetByIdAsync(int id);
        Task<IEnumerable<PersonViewModel>> GetAllAsync();
        Task AddAsync(PersonViewModel entity);
        Task UpdateAsync(PersonViewModel entity);
        Task DeleteAsync(PersonViewModel entity);
        Task<PersonViewModel> GetByIdWithTransactionsAsync(int id);
        Task<IEnumerable<PersonViewModel>> GetAllWithTransactionsAsync();
        Task<bool> ExistsAsync(int id);
    }
}
