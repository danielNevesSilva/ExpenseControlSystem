using ExpenseControlSystem.Domain.Interfaces.IoC;
using ExpenseControlSystem.Infrastructure.Data;
using System.Threading.Tasks;

namespace ExpenseControlSystem.Infrastructure.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly Context _context;

        public UnitOfWork(Context context)
        {
            _context = context;
        }

        public async Task<bool> Commit()
        {
            var changes = _context.SaveChangesAsync();

            return await changes > 0;

        }
    }
}
