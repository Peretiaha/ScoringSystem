using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScoringSystem.Web.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(20), MinLength(6)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(20), MinLength(6)]
        [Compare("NewPassword", ErrorMessage = "Not compare password")]
        public string ConfirmNewPassword { get; set; }
    }
}
