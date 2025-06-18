using System.ComponentModel.DataAnnotations;

namespace BankAPITest.Entities;

/// <summary>
/// Base class for entities in the application.
/// </summary>
public class EntityBase : IEntityBase
{
    /// <summary>
    /// Identifier
    /// </summary>
    [Required]
    public virtual int Id { get; set; }

    /// <summary>
    /// Equals method that checks if the Id of the current entity is equal to the Id of another entity.
    /// </summary>
    /// <param name="obj">entity object</param>
    /// <returns>Is equals or not</returns>
    public override bool Equals(object obj)
    {
        if (obj is EntityBase entityBaseObject)
        {
            return entityBaseObject.Id.Equals(Id);
        }
        return base.Equals(obj);
    }

    /// <summary>
    /// Returns the hash code for the entity based on its Id.
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    /// <summary>
    /// ToString method that returns the Id of the entity as a string.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Id.ToString();
    }
}