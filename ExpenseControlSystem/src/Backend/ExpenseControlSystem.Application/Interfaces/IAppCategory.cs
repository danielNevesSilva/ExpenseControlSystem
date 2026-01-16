
using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Domain.Enums;

namespace ExpenseControlSystem.Application.Interfaces
{
    public interface IAppCategory : IDisposable
    {
        Task<IEnumerable<CategoryViewModel>> GetByPurposeAsync(PurposeType purpose);
    }
}
