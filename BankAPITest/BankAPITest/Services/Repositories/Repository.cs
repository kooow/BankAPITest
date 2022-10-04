using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BankAPITest.Services.IRepositories;
using System.Linq.Expressions;
using BankAPITest.Entities;

namespace BankAPITest.Services.Repositories
{

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntityBase
    {

        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
        {
            return Context.Set<TEntity>().Where(filter).ToList();
        }

        public int GetCount()
        {
            return Context.Set<TEntity>().Count();
        }

        public int GetCount(Expression<Func<TEntity, bool>> filter)
        {
            return Context.Set<TEntity>().Where(filter).Count();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public int Add(TEntity entity)
        {
            try
            {
                Context.Set<TEntity>().Add(entity);
            }
            catch (Exception)
            {
                return -1;
            }
            return 0;
        }

        public void Remove(int id)
        {
            try
            {
                var entity = Context.Set<TEntity>().FirstOrDefault(t => t.Id == id);
                if (entity != null)
                {
                    Context.Set<TEntity>().Remove(entity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}