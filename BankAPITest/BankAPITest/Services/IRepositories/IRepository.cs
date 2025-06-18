using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BankAPITest.Services.IRepositories;

/// <summary>
/// pository interface for basic CRUD operations on entities.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Retrieves a single entity by its identifier.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Entity</returns>
    TEntity Get(int id);

    /// <summary>
    /// Returns all entities of type TEntity.
    /// </summary>
    /// <returns>Entities</returns>
    IEnumerable<TEntity> GetAll();

    /// <summary>
    /// Returns all entities of type TEntity that match the specified filter.
    /// </summary>
    /// <param name="filter">filter expression</param>
    /// <returns>Entities</returns>
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter);

    /// <summary>
    /// Return the count of all entities of type TEntity.
    /// </summary>
    /// <returns>count of all entities</returns>
    int GetCount();

    /// <summary>
    /// Returns the count of entities of type TEntity that match the specified filter.
    /// </summary>
    /// <param name="filter">filter expression</param>
    /// <returns>count of all entities</returns>
    int GetCount(Expression<Func<TEntity, bool>> filter);

    /// <summary>
    /// Adds a new entity to the repository and returns success or failure.
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <returns>Result of the adding</returns>
    int Add(TEntity entity);

    /// <summary>
    /// Deletes a single entity with given id
    /// </summary>
    /// <param name="entityId">The id of entity to delete</param>
    void Remove(int entityId);
}

