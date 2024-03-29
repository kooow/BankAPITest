﻿using System;
using System.Linq;
using BankAPITest.Entities;
using System.Collections.Generic;
using BankAPITest.Services.IRepositories;

namespace BankAPITest.Services.Repositories
{

    public class TransactionDataRepository : Repository<TransactionData>, ITransactionDataRepository
    {
        public TransactionDataRepository(APIDbContext context) : base(context) { }

        public bool CreateTransfer(int userId, int accountNumberFrom, int accountNumberTo, decimal amount, string comment)
        {
            var apiDbContext = Context as APIDbContext;

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
                TransactionType = TransactionType.Withdrawal.ToString(),
                Amount = -amount,
                CurrentBalance = fromAccount.Balance - amount,
                Comment = comment,
            };

            var transaction1To = new TransactionData()
            {
                AccountNumber = accountNumberTo,
                Date = DateTime.Now,
                TransactionType = TransactionType.Deposit.ToString(),
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
}


