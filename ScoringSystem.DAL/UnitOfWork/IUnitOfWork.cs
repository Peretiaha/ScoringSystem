using ScoringSystem.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringSystem.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Commit();

        IRepository<T> GetRepository<T>() where T : class;
    }
}
