
using ExpenseControlSystem.Domain.Enums;

namespace ExpenseControlSystem.Application.ViewModel
{
    public class TransactionViewModel : BaseEntityViewModel
    {
        public string Description { get;  set; }
        public decimal Amount { get;  set; }
        public TransactionType Type { get;  set; }
        public int CategoryId { get;  set; }
        public int PersonId { get;  set; }
        public DateTime CreatedAt { get;  set; }

        public string CategoryDescription { get; set; }
        public string PersonName { get; set; }
        public PersonViewModel Person { get;  set; }
        public CategoryViewModel Category { get;  set; }
    }
}
