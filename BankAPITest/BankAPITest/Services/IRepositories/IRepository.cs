using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BankAPITest.Services.IRepositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);

        int GetCount();

        int GetCount(Expression<Func<TEntity, bool>> filter);

        int Add(TEntity entity);

        /// <summary>
        /// Deletes a single entity with given id
        /// </summary>
        /// <param name="entityId">The id of entity to delete</param>
        void Remove(int entityId);
    }

}

