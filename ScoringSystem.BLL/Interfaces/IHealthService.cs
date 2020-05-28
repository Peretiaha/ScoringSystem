using ScoringSystem.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringSystem.BLL.Interfaces
{
    public interface IHealthService: IService<Health>
    {
        Health GetById(int id);

        IEnumerable<Health> GetAll(int userId);
    }
}
