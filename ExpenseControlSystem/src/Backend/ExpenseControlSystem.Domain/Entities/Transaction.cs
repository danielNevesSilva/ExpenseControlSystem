using ExpenseControlSystem.Domain.Enums;

namespace ExpenseControlSystem.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public string Description { get;  set; }
        public decimal Amount { get;  set; }
        public TransactionType Type { get;  set; }
        public int CategoryId { get;  set; }
        public Category Category { get;  set; }
        public int PersonId { get;  set; }
        public Person Person { get;  set; }
        public DateTime CreatedAt { get;  set; }

        public Transaction() { } 

        public Transaction(
            string description,
            decimal amount,
            TransactionType type,
            int categoryId,
            int personId,
            Person person = null,
            Category category = null)
        {
            Description = description;
            Amount = amount;
            Type = type;
            CategoryId = categoryId;
            PersonId = personId;
            Person = person;
            Category = category;
            CreatedAt = DateTime.UtcNow;

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Description))
                throw new DomainException("Descrição é obrigatória");

            if (Amount <= 0)
                throw new DomainException("Valor deve ser positivo");
        }
    }
}
