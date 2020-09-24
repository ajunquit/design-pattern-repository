using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace design_pattern_repository.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        TEntity Get(int id);
        TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

    }
}
