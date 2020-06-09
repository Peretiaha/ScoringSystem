using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoringSystem.Web.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Photo { get; set; }

        public string Email { get; set; }

        public string Country { get; set; }

        public int WeightAverage { get; set; }

        public int HeartRateAverage { get; set; }

        public int BilurubinAverage { get; set; }

        public DateTime LastanalizDate { get; set; }

        public decimal TotalDebt { get; set; }

        public decimal TotalCredit { get; set; }
    }
}
