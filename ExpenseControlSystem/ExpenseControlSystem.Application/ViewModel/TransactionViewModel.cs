using ExpenseControlSystem.Application.Enum;

namespace ExpenseControlSystem.Application.ViewModel
{
    public class TransactionViewModel : BaseEntityViewModel
    {
        public string Description { get; private set; }
        public decimal Amount { get; private set; }
        public TransactionType Type { get; private set; }
        public int CategoryId { get; private set; }
        public CategoryViewModel Category { get; private set; }
        public int PersonId { get; private set; }
        public PersonViewModel Person { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}
