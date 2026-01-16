using ExpenseControlSystem.Domain.Entities;
using ExpenseControlSystem.Domain.Interfaces.IoC;
using ExpenseControlSystem.Domain.Interfaces.Repositories;
using ExpenseControlSystem.Domain.Interfaces.Service;

namespace ExpenseControlSystem.Domain.Services
{
    public class ServicePerson : ServiceBase<Person>, IServicePerson
    {
        private readonly IPersonRepository _repository;
        public ServicePerson(IPersonRepository personRepository, IUnitOfWork unitOfWork) : base(personRepository, unitOfWork)
        {
            _repository = personRepository;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _repository.ExistsAsync(id);
        }

        public async Task<IEnumerable<Person>> GetAllWithTransactionsAsync()
        {
            return await _repository.GetAllWithTransactionsAsync();
        }

        public Task<Person> GetByIdWithTransactionsAsync(int id)
        {
            return _repository.GetByIdWithTransactionsAsync(id);
        }
    }
}
