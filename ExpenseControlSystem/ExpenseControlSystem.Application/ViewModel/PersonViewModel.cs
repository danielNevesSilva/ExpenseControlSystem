using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControlSystem.Application.ViewModel
{
    public class PersonViewModel : BaseEntityViewModel
    {
        public string Name { get; private set; }
        public int Age { get; private set; }
        public ICollection<TransactionViewModel> Transactions { get; private set; } = new List<TransactionViewModel>();
    }
}
