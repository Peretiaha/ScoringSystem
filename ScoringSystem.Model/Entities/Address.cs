using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringSystem.Model.Entities
{
    public class Address
    {
        public int AddressId { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string PostCode { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string StateOrProvince { get; set; }

        public User User { get; set; }
    }
}
