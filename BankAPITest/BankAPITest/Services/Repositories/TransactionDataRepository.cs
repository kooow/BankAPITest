using BankAPITest.Entities;
using BankAPITest.Services.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BankAPITest.Services.Repositories;

/// <summary>
/// Repository for managing transaction data
/// </summary>
public class TransactionDataRepository : Repository<TransactionData>, ITransactionDataRepository
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context">Database context</param>
    public TransactionDataRepository(APIDbContext context) : base(context) { }

    /// <inheritdoc/>
    public bool CreateTransfer(int userId, int accountNumberFrom, int accountNumberTo, decimal amount, string comment)
    {
        APIDbContext? apiDbContext = Context as APIDbContext;
        if (apiDbContext is null)
        {
            throw new InvalidOperationException($"Context is not of type {nameof(APIDbContext)}.");
        }

        var fromAccount = (from a in apiDbContext.Accounts
                           where a.User.Id == userId && a.AccountNumber == accountNumberFrom
                           select a).FirstOrDefault();

        var toAccount = (from a in apiDbContext.Accounts
                         where a.User.Id == userId && a.AccountNumber == accountNumberFrom
                         select a).FirstOrDefault();

        var transaction1From = new TransactionData()
        {
            AccountNumber = accountNumberFrom,
            Date = DateTime.Now,
            TransactionType = nameof(TransactionType.Withdrawal),
            Amount = -amount,
            CurrentBalance = fromAccount.Balance - amount,
            Comment = comment,
        };

        var transaction1To = new TransactionData()
        {
            AccountNumber = accountNumberTo,
            Date = DateTime.Now,
            TransactionType = nameof(TransactionType.Deposit),
            Amount = amount,
            CurrentBalance = fromAccount.Balance + amount,
            Comment = comment,
        };

        apiDbContext.Transactions.Add(transaction1From);
        apiDbContext.Transactions.Add(transaction1To);
        apiDbContext.SaveChanges();

        // TODO: error handling

        return true;
    }

    /// <inheritdoc/>
    public IEnumerable<TransactionData> GetUserTransactions(int userId, bool orderByDate)
    {
        var apiDbContext = Context as APIDbContext;

        IQueryable<TransactionData> transactions = from a in apiDbContext.Accounts
                                                   join t in apiDbContext.Transactions
                                                              on a.AccountNumber equals t.AccountNumber
                                                   where a.User.Id == userId
                                                   select t;

        if (orderByDate)
        {
            transactions = transactions.OrderBy(tr => tr.Date);
        }
        else
        {
            transactions = transactions.OrderByDescending(tr => tr.Date);
        }

        return transactions.ToList();
    }

    /// <inheritdoc/>
    public IEnumerable<TransactionData> GetUserTransactionsFiltered(int userId, TransactionType? transactionType, DateTime? dateFrom, DateTime? dateTo)
    {
        var apiDbContext = Context as APIDbContext;

        IQueryable<TransactionData> transactions =
            from a in apiDbContext.Accounts
            join t in apiDbContext.Transactions
                       on a.AccountNumber equals t.AccountNumber
            where a.User.Id == userId
            select t;

        if (transactionType.HasValue)
        {
            transactions = transactions.Where(t => t.TransactionType == transactionType.Value.ToString());
        }

        if (dateFrom.HasValue)
        {
            transactions = transactions.Where(t => t.Date >= dateFrom.Value);
        }

        if (dateTo.HasValue)
        {
            transactions = transactions.Where(t => t.Date <= dateTo.Value);
        }

        return transactions.ToList();
    }
}


