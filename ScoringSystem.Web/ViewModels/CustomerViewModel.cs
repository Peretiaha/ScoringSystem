using ScoringSystem.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoringSystem.Web.ViewModels
{
    public class CustomerViewModel
    {
        public int? UserId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public DateTime Birthday { get; set; }

        public string Email { get; set; }

        public byte[] Photo { get; set; }

        public IEnumerable<UsersHealth> UsersHealth { get; set; }

        public Address Address { get; set; }

        public int? AddressId { get; set; }

        public IEnumerable<BankAccount> BankAccounts { get; set; }

        public IEnumerable<UsersRoles> UsersRoles { get; set; }
    }
}
