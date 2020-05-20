using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringSystem.DAL.Repository
{
    public interface IRepositoryFactory
    {
        IRepository<T> GetRepository<T>() where T : class;
    }
}
