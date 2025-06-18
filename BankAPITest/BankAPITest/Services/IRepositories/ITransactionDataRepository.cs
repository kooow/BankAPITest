using BankAPITest.Entities;
using System;
using System.Collections.Generic;

namespace BankAPITest.Services.IRepositories;

/// <summary>
/// Defines methods for managing and retrieving transaction data for users.
/// </summary>
public interface ITransactionDataRepository
{
    /// <summary>
    /// Creates a new transaction for a user. Two accounts are involved in the transfer.
    /// </summary>
    /// <param name="userId">User id</param>
    /// <param name="accountNumberFrom">Account number (from)</param>
    /// <param name="accountNumberTo">Account number (to)</param>
    /// <param name="amount">Amount</param>
    /// <param name="comment">Comment</param>
    /// <returns>Result of the transaction creation</returns>
    bool CreateTransfer(int userId, int accountNumberFrom, int accountNumberTo, decimal amount, string comment);

    /// <summary>
    /// Returns all transactions for a user, optionally ordered by date.
    /// </summary>
    /// <param name="userId">User id</param>
    /// <param name="orderByDate">Is ordered by date?</param>
    /// <returns>Transactions</returns>
    IEnumerable<TransactionData> GetUserTransactions(int userId, bool orderByDate);

    /// <summary>
    /// Returns all transactions for a user, filtered by transaction type and date range.
    /// </summary>
    /// <param name="userId">User id</param>
    /// <param name="transactionType">Transaction type</param>
    /// <param name="dateFrom">Date from</param>
    /// <param name="dateTo">Date to</param>
    /// <returns>Transactions</returns>
    IEnumerable<TransactionData> GetUserTransactionsFiltered(int userId, TransactionType? transactionType, DateTime? dateFrom, DateTime? dateTo);
}
