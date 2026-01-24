using ExpenseControlSystem.Domain.Entities;
using ExpenseControlSystem.Domain.Enums;

namespace ExpenseControlSystem.Domain.Interfaces.Service
{
    public interface IServiceCategory : IServiceBase<Category>
    {
        Task<IEnumerable<Category>> GetByPurposeAsync(PurposeType purpose);
    }
}
