using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControlSystem.Domain.Interfaces.Service
{
    public interface IServiceBase<T> where T : class
    {
       public Task<T> GetByIdAsync(int id);
       public Task<IEnumerable<T>> GetAllAsync();
       public void AddAsync(T entity);
       public void UpdateAsync(T entity);
       public void DeleteAsync(T entity);
    }
}
