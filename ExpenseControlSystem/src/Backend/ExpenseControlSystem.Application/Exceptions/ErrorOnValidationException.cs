using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControlSystem.Application.Exceptions
{
    public class ErrorOnValidationException : Exception
    {
        public IList<string> ErrorMessages { get; set; }

        public ErrorOnValidationException(IList<string> errorMessages)
         : base(string.Join("; ", errorMessages)) // Define a Message base
        {
            ErrorMessages = errorMessages;
        }

        public ErrorOnValidationException(string errorMessage)
            : base(errorMessage)
        {
            ErrorMessages = new List<string> { errorMessage };
        }

    }
}
