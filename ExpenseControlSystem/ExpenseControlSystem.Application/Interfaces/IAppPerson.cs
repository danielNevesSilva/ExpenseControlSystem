using ExpenseControlSystem.Application.ViewModel;

namespace ExpenseControlSystem.Application.Interfaces
{
    public interface IAppPerson : IDisposable
    {
        PersonViewModel GetByIdAsync(int id);
        Task<IEnumerable<PersonViewModel>> GetAllAsync();
        Task AddAsync(PersonViewModel entity);
        Task UpdateAsync(PersonViewModel entity);
        Task DeleteAsync(PersonViewModel entity);
    }
}
