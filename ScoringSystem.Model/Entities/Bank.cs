using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringSystem.Model.Entities
{
    public class Bank
    {
        public int BankId { get; set; }

        public string Name { get; set; }

        public string LinkToSite { get; set; }

        public IEnumerable<BankAccount> BankAccounts { get; set; }
    }
}
