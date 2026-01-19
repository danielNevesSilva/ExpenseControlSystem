using ExpenseControlSystem.Domain.Enums;

namespace ExpenseControlSystem.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Description { get;  set; }
        public PurposeType Purpose { get;  set; }

        public Category(){}
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
