using Bogus;
using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommumUtilTest.Transaction
{
    public class TransactionViewModelBuilder
    {
        private readonly Faker<TransactionViewModel> _faker;
        private TransactionViewModel _customTransaction;

        public TransactionViewModelBuilder()
        {
            _faker = new Faker<TransactionViewModel>("pt_BR")
                .RuleFor(t => t.Id, f => f.Random.Int(1, 1000))
                .RuleFor(t => t.Description, f => f.Commerce.ProductName())
                .RuleFor(t => t.Amount, f => f.Random.Decimal(1, 5000))
                .RuleFor(t => t.Type, f => f.PickRandom<TransactionType>())
                .RuleFor(t => t.CategoryId, f => f.Random.Int(1, 10))
                .RuleFor(t => t.PersonId, f => f.Random.Int(1, 10))
                .RuleFor(t => t.CreatedAt, f => f.Date.Past())
                .RuleFor(t => t.UpdatedAt, f => f.Date.Recent().OrNull(f));
        }

        public TransactionViewModelBuilder WithId(int id)
        {
            _customTransaction ??= new TransactionViewModel();
            _customTransaction.Id = id;
            return this;
        }

        public TransactionViewModelBuilder WithDescription(string description)
        {
            _customTransaction ??= new TransactionViewModel();
            _customTransaction.Description = description;
            return this;
        }

        public TransactionViewModelBuilder WithAmount(decimal amount)
        {
            _customTransaction ??= new TransactionViewModel();
            _customTransaction.Amount = amount;
            return this;
        }

        public TransactionViewModelBuilder WithType(TransactionType type)
        {
            _customTransaction ??= new TransactionViewModel();
            _customTransaction.Type = type;
            return this;
        }

        public TransactionViewModelBuilder AsExpense()
        {
            return WithType(TransactionType.Expense);
        }

        public TransactionViewModelBuilder AsIncome()
        {
            return WithType(TransactionType.Income);
        }

        public TransactionViewModelBuilder WithCategoryId(int categoryId)
        {
            _customTransaction ??= new TransactionViewModel();
            _customTransaction.CategoryId = categoryId;
            return this;
        }

        public TransactionViewModelBuilder WithPersonId(int personId)
        {
            _customTransaction ??= new TransactionViewModel();
            _customTransaction.PersonId = personId;
            return this;
        }

        public TransactionViewModelBuilder WithInvalidDescription()
        {
            _customTransaction ??= new TransactionViewModel();
            _customTransaction.Description = "AB"; // Muito curto
            return this;
        }

        public TransactionViewModelBuilder WithInvalidAmount()
        {
            _customTransaction ??= new TransactionViewModel();
            _customTransaction.Amount = 0; // Valor inválido
            return this;
        }

        public TransactionViewModelBuilder WithNegativeAmount()
        {
            _customTransaction ??= new TransactionViewModel();
            _customTransaction.Amount = -10; // Valor negativo
            return this;
        }

        public TransactionViewModel Build()
        {
            if (_customTransaction != null)
            {
                var transaction = _customTransaction;
                _customTransaction = null;
                return transaction;
            }

            return _faker.Generate();
        }

        public List<TransactionViewModel> BuildList(int count = 5)
        {
            return _faker.Generate(count);
        }

        // Métodos estáticos
        public static TransactionViewModel Valid()
        {
            return new TransactionViewModelBuilder().Build();
        }

        public static TransactionViewModel Expense()
        {
            return new TransactionViewModelBuilder().AsExpense().Build();
        }

        public static TransactionViewModel Income()
        {
            return new TransactionViewModelBuilder().AsIncome().Build();
        }

        public static TransactionViewModel WithInvalidData()
        {
            return new TransactionViewModelBuilder()
                .WithInvalidDescription()
                .WithInvalidAmount()
                .Build();
        }

        // Cenários específicos para testes de regra de negócio
        public static TransactionViewModel ExpenseForMinor()
        {
            return new TransactionViewModelBuilder()
                .AsExpense()
                .WithPersonId(1) // Será mockado como menor
                .WithCategoryId(1) // Categoria Expense
                .Build();
        }

        public static TransactionViewModel IncomeForMinor()
        {
            return new TransactionViewModelBuilder()
                .AsIncome()
                .WithPersonId(1) // Será mockado como menor
                .WithCategoryId(2) // Categoria Income
                .Build();
        }
    }
}
