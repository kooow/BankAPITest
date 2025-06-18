using BankAPITest.Entities;
using BankAPITest.Services.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BankAPITest.Services.Repositories;

/// <summary>
/// Generic repository for basic CRUD operations on entities.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntityBase
{
    protected readonly DbContext Context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context">Database context</param>
    public Repository(DbContext context)
    {
        Context = context;
    }

    /// <inheritdoc/>
    public TEntity Get(int id)
    {
        return Context.Set<TEntity>().Find(id);
    }

    /// <inheritdoc/>
    public IEnumerable<TEntity> GetAll()
    {
        return Context.Set<TEntity>().ToList();
    }

    /// <inheritdoc/>
    public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter)
    {
        return Context.Set<TEntity>().Where(filter).ToList();
    }

    /// <inheritdoc/>
    public int GetCount()
    {
        return Context.Set<TEntity>().Count();
    }

    /// <inheritdoc/>
    public int GetCount(Expression<Func<TEntity, bool>> filter)
    {
        return Context.Set<TEntity>().Where(filter).Count();
    }

    /// <inheritdoc/>
    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return Context.Set<TEntity>().Where(predicate);
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public void Remove(int id)
    {
        try
        {
            var entity = Context.Set<TEntity>().FirstOrDefault(t => t.Id == id);
            if (entity is not null)
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