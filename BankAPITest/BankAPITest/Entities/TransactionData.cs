using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAPITest.Entities;

/// <summary>
/// TransactionData entity representing a user's transaction details
/// </summary>
public class TransactionData : IEntityBase
{
    /// <summary>
    /// Identifier for the transaction data
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Account number associated with the transaction.
    /// </summary>
    [ForeignKey("AccountNumber")]
    public int AccountNumber { get; set; }

    /// <summary>
    /// Transaction type, e.g., "Deposit", "Withdrawal", etc.
    /// </summary>
    public string? TransactionType { get; set; }

    /// <summary>
    /// Date of the transaction.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Amount of money involved in the transaction.
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Current balance after the transaction.
    /// </summary>
    public decimal CurrentBalance { get; set; }

    /// <summary>
    /// Comment or description of the transaction.
    /// </summary>
    public string? Comment { get; set; }
}
