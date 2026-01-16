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

        public void AddAsync(T entity)
        {
            _repositoryBase.AddAsync(entity);
            _unitOfWork.Commit();
        }

        public void DeleteAsync(T entity)
        {
            _repositoryBase.DeleteAsync(entity);
            _unitOfWork.Commit();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return _repositoryBase.GetAllAsync();

        }

        public Task<T> GetByIdAsync(int id)
        {
            return _repositoryBase.GetByIdAsync(id);
        }

        public void UpdateAsync(T entity)
        {
            _repositoryBase.UpdateAsync(entity);
            _unitOfWork.Commit();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(true);
        }
    }
}
