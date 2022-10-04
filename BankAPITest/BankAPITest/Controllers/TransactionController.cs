using BankAPITest.Entities;
using BankAPITest.Services.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BankAPITest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public class TransactionController : ControllerBase
    {

        private readonly ILogger<TransactionController> _logger;

        private readonly ITransactionDataRepository _transactionsRepository;

        private readonly IAccountsRepository _accountsRepository;

        public TransactionController(ILogger<TransactionController> logger, ITransactionDataRepository transactionRepository, IAccountsRepository accountsRepository)
        {
            this._logger = logger;
            this._transactionsRepository = transactionRepository;
            this._accountsRepository = accountsRepository;
        }


        [HttpGet("TransactionsHistory")]
        [ProducesResponseType(typeof(IEnumerable), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IEnumerable<TransactionData> GetTransactionsHistory()
        {
            var transactions = _transactionsRepository.GetUserTransactions(Global.TestUserId, true);
            return transactions;
        }

        [HttpGet("TransactionsFiltered")]
        [ProducesResponseType(typeof(IEnumerable), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IEnumerable<TransactionData> GetTransactionsFiltered(TransactionType? type, DateTime? from, DateTime? to)
        {
            var tranactions = _transactionsRepository.GetUserTransactionsFiltered(Global.TestUserId, type, from, to);
            return tranactions;
        }

        [HttpPost("CreateTransfer")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public string CreateTransfer(int accountNumberFrom, int accountNumberTo, decimal amount, string comment)
        {
            // TODO: validation

            // TODO: accountRepository - possible to transfer

            bool transferResult = _transactionsRepository.CreateTransfer(Global.TestUserId, accountNumberFrom, accountNumberTo, amount, comment);

            if (!transferResult)
            {
                return "Transaction error";
            }

            return "Successfully created transaction";
        }

        // from wallet to account

        [HttpPost("CreateDeposit")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
 
        public string CreateDeposit(int targetAccountNumber, decimal amount)
        {
            var walletAccount = _accountsRepository.GetWalletByUser(Global.TestUserId);
            bool transferResult = _transactionsRepository.CreateTransfer(Global.TestUserId, walletAccount.AccountNumber, targetAccountNumber, amount, "deposit");

            if (!transferResult)
            {
                return "Transaction error";
            }

            return "Successfully created transaction";
        }

        // from account to wallet - 
        [HttpPost("CreateWithdrawal")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]

        public string CreateWithdrawal(int fromAccountNumber, decimal amount)
        {
            var walletAccount = _accountsRepository.GetWalletByUser(Global.TestUserId);

            bool transferResult = _transactionsRepository.CreateTransfer(Global.TestUserId, fromAccountNumber, walletAccount.AccountNumber, amount, "withdrawal");
            if (!transferResult)
            {
                return "Transaction error";
            }

            return "Successfully created transaction";
        }

    }

}