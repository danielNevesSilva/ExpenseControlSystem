using ExpenseControlSystem.Domain.Enums;

namespace ExpenseControlSystem.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public string Description { get; private set; }
        public decimal Amount { get; private set; }
        public TransactionType Type { get; private set; }
        public int CategoryId { get; private set; }
        public Category Category { get; private set; }
        public int PersonId { get; private set; }
        public Person Person { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Transaction() { } // Para EF

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
