using ExpenseControlSystem.Domain.Interfaces.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommumUtilTest.Repository
{
    public class RepositoryTransactionBuilder
    {
        private readonly Mock<ITransactionRepository> _repository;

        public RepositoryTransactionBuilder()
        {
            _repository = new Mock<ITransactionRepository>();
        }
    }
}
