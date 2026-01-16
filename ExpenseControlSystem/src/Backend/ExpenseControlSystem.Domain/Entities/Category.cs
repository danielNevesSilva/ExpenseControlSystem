using ExpenseControlSystem.Domain.Enums;

namespace ExpenseControlSystem.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Description { get; private set; }
        public PurposeType Purpose { get; private set; }

        public Category(string description, PurposeType purpose)
        {
            Description = description;
            Purpose = purpose;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Description))
                throw new DomainException("Descrição é obrigatória");
        }
    }
}
