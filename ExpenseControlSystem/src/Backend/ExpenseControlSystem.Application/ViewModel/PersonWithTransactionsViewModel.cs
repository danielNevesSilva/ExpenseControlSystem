using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControlSystem.Application.ViewModel
{
    public class PersonWithTransactionsViewModel : BaseEntityViewModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<TransactionViewModel> Transactions { get; set; } = new();
    }
}
