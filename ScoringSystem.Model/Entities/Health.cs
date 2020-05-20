using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringSystem.Model.Entities
{
    public class Health
    {
        public int HealthId { get; set; }

        public int Weight { get; set; }

        public int ArterialPressure { get; set; }

        public int NumberOfRespiratoryMovements { get; set; }

        public int HeartRate { get; set; }

        public int Hemoglobin { get; set; }

        public int Bilirubin { get; set; }

        public int BloodSugar { get; set; }

        public int WhiteBloodCells { get; set; }

        public int BodyTemperature { get; set; }

        public User User { get; set; }
    }
}
