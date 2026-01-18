using ExpenseControlSystem.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControlSystem.Application.NovaPasta
{
    public class CreateCategoryDto : BaseEntityViewModel
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public string Purpose{get; set;}
    }
}
