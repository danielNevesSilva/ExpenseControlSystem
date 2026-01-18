using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControlSystem.Domain.Interfaces.IoC
{
    public interface IUnitOfWork
    {
       Task <bool> Commit();
    }
}
