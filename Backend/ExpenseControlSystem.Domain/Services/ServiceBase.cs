using ExpenseControlSystem.Domain.Interfaces.IoC;
using ExpenseControlSystem.Domain.Interfaces.Repositories;
using ExpenseControlSystem.Domain.Interfaces.Service;

namespace ExpenseControlSystem.Domain.Services
{
    public class ServiceBase<T> : IServiceBase<T>, IDisposable where T : class
    {
        private readonly IRepositoryBase<T> _repositoryBase;
        private readonly IUnitOfWork _unitOfWork;
        public ServiceBase(IRepositoryBase<T> repositoryBase, IUnitOfWork unitOfWork)
        {
            _repositoryBase = repositoryBase;
            _unitOfWork = unitOfWork;


        }

        public async Task AddAsync(T entity)
        {
            await _repositoryBase.AddAsync(entity);
            await _unitOfWork.Commit();
        }

        public async Task DeleteAsync(T entity)
        {
            await _repositoryBase.DeleteAsync(entity);
            await _unitOfWork.Commit();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return _repositoryBase.GetAllAsync();

        }

        public Task<T> GetByIdAsync(int id)
        {
            return _repositoryBase.GetByIdAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            await _repositoryBase.UpdateAsync(entity);
            await _unitOfWork.Commit();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
