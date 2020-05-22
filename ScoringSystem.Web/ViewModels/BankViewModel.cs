using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScoringSystem.Web.ViewModels
{
    public class BankViewModel
    {
        [Required]
        [MinLength(3), MaxLength(25)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        [DataType(DataType.Url)]
        public string LinkToSite { get; set; }
    }
}
