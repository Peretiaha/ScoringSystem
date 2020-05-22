using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScoringSystem.Web.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Lastname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MaxLength(20), MinLength(6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(20), MinLength(6)]
        [Compare("Password", ErrorMessage = "Not compare password")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }

        public IEnumerable<Guid> SelectedRoles { get; set; }

        public IFormFile Photo { get; set; }
    }
}
