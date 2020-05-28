using Microsoft.AspNetCore.Mvc;
using ScoringSystem.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScoringSystem.Web.ViewModels
{
    public class HealthViewModel
    {
        public int? HealthId { get; set; }

        [Required]
        public int Weight { get; set; }

        [Required]

        public int ArterialPressure { get; set; }

        [Required]

        public int NumberOfRespiratoryMovements { get; set; }

        [Required]

        public int HeartRate { get; set; }

        [Required]
        public int Hemoglobin { get; set; }

        [Required]
        public int Bilirubin { get; set; }

        [Required]
        public int BloodSugar { get; set; }

        [Required]
        public int WhiteBloodCells { get; set; }

        [Required]
        public int BodyTemperature { get; set; }

        public DateTime AnalizDate { get; set; }

        public IEnumerable<UsersHealth> UsersHealth { get; set; }
    }
}
