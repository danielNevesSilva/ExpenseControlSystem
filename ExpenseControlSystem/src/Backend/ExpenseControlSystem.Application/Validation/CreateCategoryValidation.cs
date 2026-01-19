using ExpenseControlSystem.Application.Dto;
using ExpenseControlSystem.Application.NovaPasta;
using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControlSystem.Application.Validation
{
    public class CreateCategoryValidation : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryValidation()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Descrição é obrigatória")
                .Length(3, 100).WithMessage("Descrição deve ter entre 3 e 100 caracteres");

            RuleFor(x => x.Purpose)
                .IsInEnum().WithMessage("Finalidade inválida")
                .Must(p => p == PurposeType.Expense ||
                          p == PurposeType.Income ||
                          p == PurposeType.Both)
                .WithMessage("Finalidade deve ser: Expense, Income ou Both");
        }
    }
}
