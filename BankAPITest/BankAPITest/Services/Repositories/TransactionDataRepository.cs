using System;
using System.Linq;
using System.Linq.Expressions;
using BankAPITest.Services;
using BankAPITest.Entities;
using System.Collections.Generic;

namespace BankAPITest.Services.Repositories
{

    public class TransactionDataRepository : Repository<TransactionData>
    {
        public TransactionDataRepository(APIDbContext context) : base(context) { }

        public bool CreateTransfer(int userId, int accountNumberFrom, int accountNumberTo, decimal amount, string comment)
        {
            var apiDbContext = Context as APIDbContext;

            // TODO: validation 

            var from_account = (from a in apiDbContext.Accounts
                                where a.User.Id == userId && a.AccountNumber == accountNumberFrom
                                select a).FirstOrDefault();

            var to_account = (from a in apiDbContext.Accounts
                                where a.User.Id == userId && a.AccountNumber == accountNumberFrom
                              select a).FirstOrDefault();


            var transaction1_from = new TransactionData()
            {
                AccountNumber = accountNumberFrom,
                Date = DateTime.Now,
                TransactionType = TransactionType.Withdrawal.ToString(),
                Amount = -amount,
                CurrentBalance = from_account.Balance - amount,
                Comment = comment,
            };

            var transaction1_to = new TransactionData()
            {
                AccountNumber = accountNumberTo,
                Date = DateTime.Now,
                TransactionType = TransactionType.Deposit.ToString(),
                Amount = amount,
                CurrentBalance = from_account.Balance + amount,
                Comment = comment,
            };

            apiDbContext.Transactions.Add(transaction1_from);
            apiDbContext.Transactions.Add(transaction1_to);
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


