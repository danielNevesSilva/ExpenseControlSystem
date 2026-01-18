using ExpenseControlSystem.Domain.Entities;
using ExpenseControlSystem.Domain.Interfaces.Repositories;
using ExpenseControlSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControlSystem.Infrastructure.Repository
{
    public class RepositoryPerson : RepositoryBase<Person>, IPersonRepository
    {
        public RepositoryPerson(Context context) : base(context)
        {

        }
        public async Task<Person> GetByIdAsync(int id)
        {
            return await _context.Persons.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id);
        }

        public override async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.Persons
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        // Métodos específicos
        public async Task<Person> GetByIdWithTransactionsAsync(int id)
        {
            return await _context.Persons
                .Include(p => p.Transactions)
                .ThenInclude(t => t.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Person>> GetAllWithTransactionsAsync()
        {
            return await _context.Persons
                .Include(p => p.Transactions)
                .ThenInclude(t => t.Category)
                .ToListAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Persons.AnyAsync(p => p.Id == id);
        }

    }
}
