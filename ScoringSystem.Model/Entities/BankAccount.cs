using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringSystem.Model.Entities
{
    public class BankAccount
    {
        public int BankAccountId { get; set; }

        public User User { get; set; }

        public Bank Bank { get; set; }

        public string CardNumber { get; set; }

        public decimal Debt { get; set; }

        public decimal Credit { get; set; }
    }
}
