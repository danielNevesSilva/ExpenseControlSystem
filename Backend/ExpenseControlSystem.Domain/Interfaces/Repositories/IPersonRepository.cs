using ExpenseControlSystem.Domain.Entities;

namespace ExpenseControlSystem.Domain.Interfaces.Repositories
{
    public interface IPersonRepository : IRepositoryBase<Person>
    {
        Task<Person> GetByIdWithTransactionsAsync(int id);
        Task<IEnumerable<Person>> GetAllWithTransactionsAsync();
        Task<bool> ExistsAsync(int id);

    }


}
