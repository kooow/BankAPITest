using BankAPITest.Entities;
using System;
using System.Collections.Generic;

namespace BankAPITest.Services.IRepositories
{
    public interface ITransactionDataRepository
    {
        bool CreateTransfer(int userId, int accountNumberFrom, int accountNumberTo, decimal amount, string comment);

        IEnumerable<TransactionData> GetUserTransactions(int userId, bool orderByDate);

        IEnumerable<TransactionData> GetUserTransactionsFiltered(int userId, TransactionType? transactionType, DateTime? dateFrom, DateTime? dateTo);
    }
}
