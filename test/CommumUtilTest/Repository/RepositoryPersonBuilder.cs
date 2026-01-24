using ExpenseControlSystem.Domain.Interfaces.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommumUtilTest.Repository
{
    public class RepositoryPersonBuilder
    {
        private readonly Mock<IPersonRepository> _repository;
        public RepositoryPersonBuilder()
        {
            _repository = new Mock<IPersonRepository>();
        }
    }
}
