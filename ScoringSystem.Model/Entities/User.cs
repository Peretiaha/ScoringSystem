using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringSystem.Model.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public DateTime Birthday { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public byte[] Photo { get; set; }

        public Health UserHealth { get; set; }

        public int HealthId { get; set; }

        public Address Address { get; set; }

        public int AddressId { get; set; }


        public IEnumerable<BankAccount> BankAccounts { get; set; }

        public IEnumerable<UsersRoles> UsersRoles { get; set; }
    }
}
