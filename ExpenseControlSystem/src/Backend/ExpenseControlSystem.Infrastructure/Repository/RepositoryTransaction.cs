using ExpenseControlSystem.Domain.Entities;
using ExpenseControlSystem.Domain.Enums;
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
    public class RepositoryTransaction : RepositoryBase<Transaction>, ITransactionRepository
    {
        public RepositoryTransaction(Context context) : base(context)
        {

        }

        // Apenas métodos específicos de Transaction
        public async Task<IEnumerable<Transaction>> GetByPersonIdAsync(int personId)
        {
            return await _context.Transactions
                .Include(t => t.Category)
                .Where(t => t.PersonId == personId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.Transactions
                .Include(t => t.Person)
                .Where(t => t.CategoryId == categoryId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalIncomeByPersonAsync(int personId)
        {
            return await _context.Transactions
                .Where(t => t.PersonId == personId && t.Type == TransactionType.Income)
                .SumAsync(t => t.Amount);
        }

        public async Task<decimal> GetTotalExpenseByPersonAsync(int personId)
        {
            return await _context.Transactions
                .Where(t => t.PersonId == personId && t.Type == TransactionType.Expense)
                .SumAsync(t => t.Amount);
        }

        // Se quiser incluir relacionamentos nos métodos genéricos
        public override async Task<Transaction> GetByIdAsync(int id)
        {
            return await _context.Transactions
                .Include(t => t.Person)
                .Include(t => t.Category)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public override async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transactions
                .Include(t => t.Person)
                .Include(t => t.Category)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }
    }
}
