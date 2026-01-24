using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Domain.Enums;
using System.Reflection.Metadata.Ecma335;

namespace ExpenseControlSystem.Application.Dto
{
    public class CreateTransactionDto : BaseEntityViewModel
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public int CategoryId { get; set; }
        public int PersonId { get; set; }
        public TransactionType Type { get; set; }

    }
}
