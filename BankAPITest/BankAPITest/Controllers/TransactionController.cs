using BankAPITest.Entities;
using BankAPITest.Services.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BankAPITest.Controllers;

/// <summary>
/// Transaction controller for managing user transactions
/// </summary>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
public class TransactionController : ControllerBase
{
    private const string TransferErrorMessage = "Transaction error.";
    private const string TransferSuccessMessage = "Successfully created transaction.";

    private readonly ILogger<TransactionController> m_logger;
    private readonly ITransactionDataRepository m_transactionsRepository;
    private readonly IAccountsRepository m_accountsRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger">Logger</param>
    /// <param name="transactionRepository">Transaction repository</param>
    /// <param name="accountsRepository">Accounts repository</param>
    public TransactionController(ILogger<TransactionController> logger, ITransactionDataRepository transactionRepository, IAccountsRepository accountsRepository)
    {
        m_logger = logger;
        m_transactionsRepository = transactionRepository;
        m_accountsRepository = accountsRepository;
    }

    /// <summary>
    /// Returns all transactions for the test user.
    /// </summary>
    /// <returns>Transactions</returns>
    [HttpGet("TransactionsHistory")]
    [ProducesResponseType(typeof(IEnumerable), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IEnumerable<TransactionData> GetTransactionsHistory()
    {
        return m_transactionsRepository.GetUserTransactions(Global.TestUserId, true);
    }

    /// <summary>
    /// Returns transactions filtered by type and date range.
    /// </summary>
    /// <param name="type">Type</param>
    /// <param name="from">from (date)</param>
    /// <param name="to">to (date)</param>
    /// <returns>Transactions</returns>
    [HttpGet("TransactionsFiltered")]
    [ProducesResponseType(typeof(IEnumerable), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IEnumerable<TransactionData> GetTransactionsFiltered(TransactionType? type, DateTime? from, DateTime? to)
    {
        return m_transactionsRepository.GetUserTransactionsFiltered(Global.TestUserId, type, from, to);
    }

    /// <summary>
    /// Creates a transfer transaction between two accounts.
    /// </summary>
    /// <param name="accountNumberFrom"></param>
    /// <param name="accountNumberTo"></param>
    /// <param name="amount"></param>
    /// <param name="comment"></param>
    /// <returns>Result message</returns>
    [HttpPost("CreateTransfer")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public string CreateTransfer(int accountNumberFrom, int accountNumberTo, decimal amount, string comment)
    {
        // TODO: validation
        // TODO: accountRepository - possible to transfer

        bool transferResult = m_transactionsRepository.CreateTransfer(Global.TestUserId, accountNumberFrom, accountNumberTo, amount, comment);
        if (!transferResult)
        {
            m_logger.LogError("Transfer failed from account {From} to account {To} with amount {Amount}.", accountNumberFrom, accountNumberTo, amount);
            return TransferErrorMessage;
        }

        return TransferSuccessMessage;
    }

    /// <summary>
    /// Creates a deposit transaction from the wallet account to a specified account.
    /// </summary>
    /// <param name="targetAccountNumber"></param>
    /// <param name="amount"></param>
    /// <returns>Result message</returns>
    [HttpPost("CreateDeposit")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public string CreateDeposit(int targetAccountNumber, decimal amount)
    {
        var walletAccount = m_accountsRepository.GetWalletByUser(Global.TestUserId);
        bool transferResult = m_transactionsRepository.CreateTransfer(Global.TestUserId, walletAccount.AccountNumber, targetAccountNumber, amount, "deposit");
        if (!transferResult)
        {
            m_logger.LogError("Deposit failed from wallet account {WalletAccount} to account {TargetAccount} with amount {Amount}.", walletAccount.AccountNumber, targetAccountNumber, amount);
            return TransferErrorMessage;
        }

        return TransferSuccessMessage;
    }

    /// <summary>
    /// Creates a withdrawal transaction from a specified account to the wallet account.
    /// </summary>
    /// <param name="fromAccountNumber"></param>
    /// <param name="amount">amount</param>
    /// <returns>Result message</returns>
    [HttpPost("CreateWithdrawal")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public string CreateWithdrawal(int fromAccountNumber, decimal amount)
    {
        var walletAccount = m_accountsRepository.GetWalletByUser(Global.TestUserId);

        bool transferResult = m_transactionsRepository.CreateTransfer(Global.TestUserId, fromAccountNumber, walletAccount.AccountNumber, amount, "withdrawal");
        if (!transferResult)
        {
            m_logger.LogError("Withdrawal failed from account {FromAccount} to wallet account {WalletAccount} with amount {Amount}.", fromAccountNumber, walletAccount.AccountNumber, amount);
            return TransferErrorMessage;
        }
        return TransferSuccessMessage;
    }
}