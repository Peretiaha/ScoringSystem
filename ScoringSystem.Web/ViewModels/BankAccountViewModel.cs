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

        public CustomerViewModel CustomerViewModel { get; set; }

        [Required]
        public BankViewModel Bank { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public decimal Debt { get; set; }

        [Required]
        public decimal Credit { get; set; }
    }
}
