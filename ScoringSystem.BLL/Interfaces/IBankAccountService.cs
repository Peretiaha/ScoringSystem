using ScoringSystem.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringSystem.BLL.Interfaces
{
    public interface IBankAccountService : IService<BankAccount>
    {
        BankAccount GetById(int bankAccountId);

        IEnumerable<BankAccount> GetAll(int userId);
    }
}
