using ScoringSystem.DAL.Context;
using ScoringSystem.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoringSystem.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ScoringSystemContext _db;

        private readonly IRepositoryFactory _repositoryFactory;

        public UnitOfWork(ScoringSystemContext db, IRepositoryFactory repositoryFactory)
        {
            _db = db;
            _repositoryFactory = repositoryFactory;
        }

        public void Commit()
        {
            _db.SaveChanges();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return _repositoryFactory.GetRepository<T>();
        }
    }
}
