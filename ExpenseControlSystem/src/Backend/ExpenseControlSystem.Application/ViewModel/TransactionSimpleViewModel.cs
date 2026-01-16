using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControlSystem.Application.ViewModel
{
    public class TransactionSimpleViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } // "Expense" ou "Income"
        public string CategoryDescription { get; set; }
        public string PersonName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
