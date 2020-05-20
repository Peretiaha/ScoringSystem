using Microsoft.EntityFrameworkCore;
using ScoringSystem.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ScoringSystem.DAL.Repository
{
    public class SqlRepository<T> : IRepository<T> where T : class
    {
        private readonly ScoringSystemContext _context;
        private readonly DbSet<T> _dbSet;

        public SqlRepository(ScoringSystemContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetMany(Expression<Func<T, bool>> filter = null, Func<IEnumerable<T>, IOrderedEnumerable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            var entities = _dbSet.AsQueryable();
            if (filter != null)
            {
                entities = entities.Where(filter);
            }

            if (includes != null)
            {
                entities = includes.Aggregate(entities, (current, include) => current.Include(include));
            }

            orderBy?.Invoke(entities);

            return entities.ToList();

        }

        public T GetSingle(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            var entity = _dbSet.Where(filter);

            if (includes != null)
            {
                entity = includes.Aggregate(entity, (current, include) => current.Include(include));
            }

            return entity.FirstOrDefault();
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _context.Entry(entity).State = EntityState.Modified;

        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
