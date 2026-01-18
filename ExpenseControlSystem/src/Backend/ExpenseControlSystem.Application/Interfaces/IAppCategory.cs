
using ExpenseControlSystem.Application.NovaPasta;
using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Domain.Enums;

namespace ExpenseControlSystem.Application.Interfaces
{
    public interface IAppCategory : IDisposable
    {
        Task<CategoryViewModel> GetByIdAsync(int id);

        Task<IEnumerable<CategoryViewModel>> GetByPurposeAsync(PurposeType purpose);
        Task<IEnumerable<CategoryViewModel>> GetAllAsync();
        Task AddAsync(CreateCategoryDto entity);
        Task UpdateAsync(CategoryViewModel entity);
        Task DeleteAsync(CategoryViewModel entity);

    }
}
