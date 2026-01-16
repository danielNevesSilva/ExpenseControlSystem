using ExpenseControlSystem.Application.ViewModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControlSystem.Application.Validation
{
    public class TransactionValidation : AbstractValidator<TransactionViewModel>
    {
        public TransactionValidation() { }
    }
}
