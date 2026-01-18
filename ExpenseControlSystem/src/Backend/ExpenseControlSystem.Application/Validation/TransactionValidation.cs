using ExpenseControlSystem.Application.ViewModel;
using FluentValidation;

namespace ExpenseControlSystem.Application.Validation
{
    public class TransactionValidation : AbstractValidator<TransactionViewModel>
    {
        public TransactionValidation()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Descrição é obrigatória")
                .Length(3, 200).WithMessage("Descrição deve ter entre 3 e 200 caracteres");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Valor deve ser maior que zero");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Tipo de transação inválido");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Categoria é obrigatória");

            RuleFor(x => x.PersonId)
                .GreaterThan(0).WithMessage("Pessoa é obrigatória");

            RuleFor(x => x.Person.Age)
                .Must(age => age >= 18)
                .WithMessage("Pessoa com menos de 18 anos não podem ter receita.")
                .When(x=>x.Type == Domain.Enums.TransactionType.Income);
        }
    }
}
