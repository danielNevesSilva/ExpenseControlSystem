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
    public class RepositoryCategory : RepositoryBase<Category>, ICategoryRepository
    {
        public RepositoryCategory(Context context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Category>> GetByPurposeAsync(PurposeType purpose)
        {
            return await _context.Categories
                .Where(c => c.Purpose == purpose || c.Purpose == PurposeType.Both)
                .OrderBy(c => c.Description)
                .ToListAsync();
        }

        public async Task<bool> IsDescriptionUniqueAsync(string description, int? excludeId = null)
        {
            var query = _context.Categories
                .Where(c => c.Description.ToLower() == description.ToLower());

            if (excludeId.HasValue)
            {
                query = query.Where(c => c.Id != excludeId.Value);
            }

            return !await query.AnyAsync();
        }

        public async Task<IEnumerable<Category>> GetCategoriesForTransactionTypeAsync(TransactionType transactionType)
        {
            PurposeType requiredPurpose = transactionType switch
            {
                TransactionType.Expense => PurposeType.Expense,
                TransactionType.Income => PurposeType.Income,
                _ => PurposeType.Both
            };

            return await _context.Categories
                .Where(c => c.Purpose == requiredPurpose || c.Purpose == PurposeType.Both)
                .OrderBy(c => c.Description)
                .ToListAsync();
        }

        public async Task<bool> HasTransactionsAsync(int categoryId)
        {
            return await _context.Transactions
                .AnyAsync(t => t.CategoryId == categoryId);
        }

    }
}
