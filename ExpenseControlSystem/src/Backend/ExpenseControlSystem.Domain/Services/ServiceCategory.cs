using ExpenseControlSystem.Domain.Entities;
using ExpenseControlSystem.Domain.Enums;
using ExpenseControlSystem.Domain.Interfaces.IoC;
using ExpenseControlSystem.Domain.Interfaces.Repositories;
using ExpenseControlSystem.Domain.Interfaces.Service;

namespace ExpenseControlSystem.Domain.Services
{
    public class ServiceCategory : ServiceBase<Category>, IServiceCategory
    {
        private readonly ICategoryRepository _repository;
        public ServiceCategory(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : base(categoryRepository, unitOfWork)
        {
            _repository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> GetByPurposeAsync(PurposeType purpose)
        {
            return await _repository.GetByPurposeAsync(purpose);
        }
    }
}
