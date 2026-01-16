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
    }
}
