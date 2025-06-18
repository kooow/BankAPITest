namespace BankAPITest.Entities;

/// <summary>
/// Common interface for entities in the application.
/// </summary>
public interface IEntityBase
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    int Id { get; set; }
}