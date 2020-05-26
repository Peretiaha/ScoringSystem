using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScoringSystem.Web.ViewModels
{
    public class BankAccountViewModel
    {
        public int? BankAccountId { get; set; }

        [Required]
        public CustomerViewModel CustomerViewModel { get; set; }
    }
}
