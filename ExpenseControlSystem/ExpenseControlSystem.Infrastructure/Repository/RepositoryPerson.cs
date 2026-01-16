using ExpenseControlSystem.Domain.Entities;
using ExpenseControlSystem.Domain.Interfaces.Repositories;
using ExpenseControlSystem.Infrastructure.Data;
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
    }
}
