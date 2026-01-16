using ExpenseControlSystem.Application.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControlSystem.Application.ViewModel
{
    public class CategoryViewModel : BaseEntityViewModel
    {
        public string Description { get; private set; }
        public PurposeType Purpose { get; private set; }
    }
}
