using CommumUtilTest.Category;
using CommumUtilTest.Transaction;
using ExpenseControlSystem.Application.Validation;
using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Domain.Enums;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControlSystem.Test.Category
{
    public class CategoryValidationTest
    {
        public class TransactionValidationTest
        {
            private readonly TransactionValidation _validator;

            public TransactionValidationTest()
            {
                _validator = new TransactionValidation();
            }

            private TransactionViewModel CreateValidModel()
            {
                return new TransactionViewModel
                {
                    Description = "Valid Transaction",
                    Amount = 100.00m,
                    Type = TransactionType.Expense,
                    CategoryId = 1,
                    PersonId = 1,
                    Person = new PersonViewModel { Name = "John Doe", Age = 30 }
                };
            }

            [Fact]
            public void Should_Pass_Validation_For_Valid_Transaction()
            {
                var model = CreateValidModel();

                ValidationResult result = _validator.Validate(model);

                Assert.True(result.IsValid);
            }

            [Fact]
            public void Should_Fail_When_Description_Is_Empty()
            {
                var model = CreateValidModel();
                model.Description = "";

                ValidationResult result = _validator.Validate(model);

                Assert.False(result.IsValid);
                Assert.Contains(result.Errors, e => e.PropertyName == "Description" && e.ErrorMessage.Contains("Descrição é obrigatória"));
            }

            [Fact]
            public void Should_Fail_When_Description_Is_Too_Short()
            {
                var model = CreateValidModel();
                model.Description = "ab";

                ValidationResult result = _validator.Validate(model);

                Assert.False(result.IsValid);
                Assert.Contains(result.Errors, e => e.PropertyName == "Description" && e.ErrorMessage.Contains("Descrição deve ter entre 3 e 200 caracteres"));
            }

            [Fact]
            public void Should_Fail_When_Description_Is_Too_Long()
            {
                var model = CreateValidModel();
                model.Description = new string('a', 201);

                ValidationResult result = _validator.Validate(model);

                Assert.False(result.IsValid);
                Assert.Contains(result.Errors, e => e.PropertyName == "Description" && e.ErrorMessage.Contains("Descrição deve ter entre 3 e 200 caracteres"));
            }

            [Fact]
            public void Should_Fail_When_Amount_Is_Zero()
            {
                var model = CreateValidModel();
                model.Amount = 0;

                ValidationResult result = _validator.Validate(model);

                Assert.False(result.IsValid);
                Assert.Contains(result.Errors, e => e.PropertyName == "Amount" && e.ErrorMessage.Contains("Valor deve ser maior que zero"));
            }

            [Fact]
            public void Should_Fail_When_Amount_Is_Negative()
            {
                var model = CreateValidModel();
                model.Amount = -10;

                ValidationResult result = _validator.Validate(model);

                Assert.False(result.IsValid);
                Assert.Contains(result.Errors, e => e.PropertyName == "Amount" && e.ErrorMessage.Contains("Valor deve ser maior que zero"));
            }

            [Fact]
            public void Should_Fail_When_Type_Is_Invalid_Enum()
            {
                var model = CreateValidModel();
                model.Type = (TransactionType)999;

                ValidationResult result = _validator.Validate(model);

                Assert.False(result.IsValid);
                Assert.Contains(result.Errors, e => e.PropertyName == "Type" && e.ErrorMessage.Contains("Tipo de transação inválido"));
            }

            [Fact]
            public void Should_Fail_When_CategoryId_Is_Zero()
            {
                var model = CreateValidModel();
                model.CategoryId = 0;

                ValidationResult result = _validator.Validate(model);

                Assert.False(result.IsValid);
                Assert.Contains(result.Errors, e => e.PropertyName == "CategoryId" && e.ErrorMessage.Contains("Categoria é obrigatória"));
            }

            [Fact]
            public void Should_Fail_When_PersonId_Is_Zero()
            {
                var model = CreateValidModel();
                model.PersonId = 0;

                ValidationResult result = _validator.Validate(model);

                Assert.False(result.IsValid);
                Assert.Contains(result.Errors, e => e.PropertyName == "PersonId" && e.ErrorMessage.Contains("Pessoa é obrigatória"));
            }

            [Fact]
            public void Should_Fail_When_Person_Is_Under_18_And_Type_Is_Income()
            {
                var model = CreateValidModel();
                model.Type = TransactionType.Income;
                model.Person = new PersonViewModel { Name = "Young Person", Age = 17 };

                ValidationResult result = _validator.Validate(model);

                Assert.False(result.IsValid);
                Assert.Contains(result.Errors, e => e.PropertyName == "Person.Age" && e.ErrorMessage.Contains("Pessoa com menos de 18 anos não podem ter receita."));
            }

            [Fact]
            public void Should_Pass_When_Person_Is_18_And_Type_Is_Income()
            {
                var model = CreateValidModel();
                model.Type = TransactionType.Income;
                model.Person = new PersonViewModel { Name = "Adult", Age = 18 };

                ValidationResult result = _validator.Validate(model);

                Assert.True(result.IsValid);
            }

            [Fact]
            public void Should_Pass_When_Person_Is_Over_18_And_Type_Is_Income()
            {
                var model = CreateValidModel();
                model.Type = TransactionType.Income;
                model.Person = new PersonViewModel { Name = "Older Adult", Age = 25 };

                ValidationResult result = _validator.Validate(model);

                Assert.True(result.IsValid);
            }

            [Fact]
            public void Should_Not_Validate_Person_Age_When_Type_Is_Expense()
            {
                var model = CreateValidModel();
                model.Type = TransactionType.Expense;
                model.Person = new PersonViewModel { Name = "Teen", Age = 15 };

                ValidationResult result = _validator.Validate(model);

                Assert.True(result.IsValid);
            }
        }
    }
}

