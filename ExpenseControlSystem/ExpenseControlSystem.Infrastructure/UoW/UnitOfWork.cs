using ExpenseControlSystem.Domain.Interfaces.IoC;
using ExpenseControlSystem.Infrastructure.Data;

namespace ExpenseControlSystem.Infrastructure.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly Context _context;

        public UnitOfWork(Context context)
        {
            _context = context;
        }

        public bool Commit()
        {
            var changes = _context.SaveChanges();

            return changes > 0;

        }
    }
}
