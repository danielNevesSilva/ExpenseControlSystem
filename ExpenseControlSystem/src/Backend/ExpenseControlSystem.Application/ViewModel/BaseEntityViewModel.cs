using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseControlSystem.Application.ViewModel
{
    public class BaseEntityViewModel
    {
        public int Id { get;  set; }
        public DateTime CreatedAt { get;  set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get;  set; }
    }
}
