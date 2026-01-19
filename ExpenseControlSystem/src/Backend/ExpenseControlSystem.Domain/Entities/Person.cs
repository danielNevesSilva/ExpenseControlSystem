using System.Transactions;

namespace ExpenseControlSystem.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();

        // Construtor protegido para EF
        public Person() { }

        public Person(string name, int age)
        {
            Name = name;
            Age = age;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new DomainException("Nome é obrigatório");

            if (Age <= 0)
                throw new DomainException("Idade deve ser positiva");
        }
    }
}

