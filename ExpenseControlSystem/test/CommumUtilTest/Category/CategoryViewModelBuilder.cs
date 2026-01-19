using Bogus;
using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommumUtilTest.Category
{
    public class CategoryViewModelBuilder
    {
        private readonly Faker<CategoryViewModel> _faker;
        private CategoryViewModel _customCategory;

        public CategoryViewModelBuilder()
        {
            _faker = new Faker<CategoryViewModel>("pt_BR")
                .RuleFor(c => c.Id, f => f.Random.Int(1, 100))
                .RuleFor(c => c.Description, f => f.Commerce.Categories(1)[0])
                .RuleFor(c => c.Purpose, f => f.PickRandom<PurposeType>())
                .RuleFor(c => c.CreatedAt, f => f.Date.Past())
                .RuleFor(c => c.UpdatedAt, f => f.Date.Recent().OrNull(f));
        }

        public CategoryViewModelBuilder WithId(int id)
        {
            _customCategory ??= new CategoryViewModel();
            _customCategory.Id = id;
            return this;
        }

        public CategoryViewModelBuilder WithDescription(string description)
        {
            _customCategory ??= new CategoryViewModel();
            _customCategory.Description = description;
            return this;
        }

        public CategoryViewModelBuilder WithPurpose(PurposeType purpose)
        {
            _customCategory ??= new CategoryViewModel();
            _customCategory.Purpose = purpose;
            return this;
        }

        public CategoryViewModelBuilder AsExpenseOnly()
        {
            return WithPurpose(PurposeType.Expense);
        }

        public CategoryViewModelBuilder AsIncomeOnly()
        {
            return WithPurpose(PurposeType.Income);
        }

        public CategoryViewModelBuilder AsBoth()
        {
            return WithPurpose(PurposeType.Both);
        }

        public CategoryViewModelBuilder WithInvalidDescription()
        {
            _customCategory ??= new CategoryViewModel();
            _customCategory.Description = "AB"; // Muito curto
            return this;
        }

        public CategoryViewModel Build()
        {
            if (_customCategory != null)
            {
                var category = _customCategory;
                _customCategory = null;
                return category;
            }

            return _faker.Generate();
        }

        public List<CategoryViewModel> BuildList(int count = 5)
        {
            return _faker.Generate(count);
        }

        // Métodos estáticos
        public static CategoryViewModel Valid()
        {
            return new CategoryViewModelBuilder().Build();
        }

        public static CategoryViewModel Expense()
        {
            return new CategoryViewModelBuilder().AsExpenseOnly().Build();
        }

        public static CategoryViewModel Income()
        {
            return new CategoryViewModelBuilder().AsIncomeOnly().Build();
        }

        public static CategoryViewModel Both()
        {
            return new CategoryViewModelBuilder().AsBoth().Build();
        }

    }
}
