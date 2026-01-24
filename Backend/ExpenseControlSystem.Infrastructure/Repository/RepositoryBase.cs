using ExpenseControlSystem.Domain.Interfaces.Repositories;
using ExpenseControlSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpenseControlSystem.Infrastructure.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T>, IDisposable where T : class
    {
        protected readonly Context _context;
        protected readonly DbSet<T> _dbSet;
        public RepositoryBase(Context context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
           await Task.FromResult( await _dbSet.AddAsync(entity));
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Attach(entity);
            _dbSet.Remove(entity);
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.FromResult(_dbSet.Update(entity));
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
