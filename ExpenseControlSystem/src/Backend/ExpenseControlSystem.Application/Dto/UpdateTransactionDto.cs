using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControlSystem.Application.Dto
{
    public class UpdateTransactionDto : BaseEntityViewModel
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public int CategoryId { get; set; }
        public int PersonId { get; set; }
    }
}
