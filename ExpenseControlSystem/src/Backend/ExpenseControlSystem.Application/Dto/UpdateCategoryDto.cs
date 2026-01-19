using ExpenseControlSystem.Application.ViewModel;
using ExpenseControlSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace ExpenseControlSystem.Application.Dto
{
    public class UpdateCategoryDto : BaseEntityViewModel
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public PurposeType Purpose { get; set; }
    }
}
