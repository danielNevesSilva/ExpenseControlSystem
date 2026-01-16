using ExpenseControlSystem.Domain.Entities;

namespace ExpenseControlSystem.Domain.Interfaces.Service
{
    public interface IServicePerson : IServiceBase<Person>
    {
        Task<Person> GetByIdWithTransactionsAsync(int id);
        Task<IEnumerable<Person>> GetAllWithTransactionsAsync();
        Task<bool> ExistsAsync(int id);
    }
}
