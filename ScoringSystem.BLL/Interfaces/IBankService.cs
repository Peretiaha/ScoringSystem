using ScoringSystem.Model.Entities;
using System.Collections.Generic;

namespace ScoringSystem.BLL.Interfaces
{
    public interface IBankService : IService<Bank>
    {
        Bank GetBankById(int id);

        IEnumerable<Bank> GetAll();
    }
}
