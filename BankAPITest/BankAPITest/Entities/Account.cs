using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankAPITest.Entities;

/// <summary>
/// Account entity representing a user's bank account
/// </summary>
public class Account : IEntityBase
{
    /// <summary>
    /// Identifier for the account
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Account name, e.g., "Wallet", "Savings", etc.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Account number, typically a unique identifier for the account
    /// </summary>
    public int AccountNumber { get; set; }

    /// <summary>
    /// Account type, represented as an integer (e.g., 0 for Wallet, 1 for Savings, etc.)
    /// </summary>
    public int AccountType { get; set; }

    /// <summary>
    /// Balance of the account, representing the current amount of money in the account
    /// </summary>
    public decimal Balance { get; set; }

    /// <summary>
    /// Modification date of the account, indicating when the account was last updated
    /// </summary>
    public DateTime ModifyDate { get; set; }

    /// <summary>
    /// User associated with the account
    /// </summary>
    [JsonIgnore]
    public User User { get; set; }

    /// <summary>
    /// Transactions associated with the account
    /// </summary>
    [JsonIgnore]
    public virtual ICollection<TransactionData> Transactions { get; set; }
}