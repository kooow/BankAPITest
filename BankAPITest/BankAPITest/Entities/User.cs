using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankAPITest.Entities;

/// <summary>
/// User entity representing a user in the banking application
/// </summary>
public class User : IEntityBase
{
    /// <summary>
    /// Identifier for the User
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// First name of the user
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Last name of the user
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Accounts associated with the user
    /// </summary>
    public virtual List<Account> Accounts { get; set; }
}