using ExpenseControlSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControlSystem.Application.ViewModel
{
    public class CategoryViewModel : BaseEntityViewModel
    {
        public string Description { get;  set; }
        public PurposeType Purpose { get;  set; }
    }
}
