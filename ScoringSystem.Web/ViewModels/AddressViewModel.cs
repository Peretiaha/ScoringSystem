using ScoringSystem.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScoringSystem.Web.ViewModels
{
    public class AddressViewModel
    {
        [Required]
        [MinLength(10), MaxLength(30)]
        public string AddressLine1 { get; set; }

        [Required]
        [MinLength(10), MaxLength(30)]
        public string AddressLine2 { get; set; }

        [Required]
        [MinLength(5), MaxLength(8)]
        public string PostCode { get; set; }

        [Required]
        [MinLength(3), MaxLength(20)]
        public string Country { get; set; }

        [Required]
        [MinLength(3), MaxLength(20)]
        public string City { get; set; }

        [Required]
        [MinLength(3), MaxLength(20)]
        public string StateOrProvince { get; set; }

        public User User { get; set; }
    }
}
