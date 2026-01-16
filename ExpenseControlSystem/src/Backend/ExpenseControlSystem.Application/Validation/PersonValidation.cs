using ExpenseControlSystem.Application.ViewModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControlSystem.Application.Validation
{
    public class PersonValidation : AbstractValidator<PersonViewModel>
    {
        public PersonValidation() {

            RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Nome é obrigatório")
                    .Length(2, 100).WithMessage("Nome deve ter entre 2 e 100 caracteres");

            RuleFor(x => x.Age)
                .GreaterThan(0).WithMessage("Idade deve ser maior que zero")
                .LessThanOrEqualTo(150).WithMessage("Idade não pode ser maior que 150 anos");
        }
    }
}
