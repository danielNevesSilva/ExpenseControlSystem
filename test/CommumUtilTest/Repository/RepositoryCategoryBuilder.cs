using ExpenseControlSystem.Domain.Interfaces.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommumUtilTest.Repository
{
    public class RepositoryCategoryBuilder
    {
        private readonly Mock<ICategoryRepository> _repository;
        public RepositoryCategoryBuilder()
        {
            _repository = new Mock<ICategoryRepository>();
        }
    }
}

