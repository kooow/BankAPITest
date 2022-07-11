using CommitSmartTest.Models;
using CommitSmartTest.Services;
using CommitSmartTest.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CommitSmartTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public class TransactionController : ControllerBase
    {

        private readonly ILogger<TransactionController> logger;

        private readonly TransactionDataRepository transactionsRepository;

        private readonly AccountsRepository accountsRepository;

        public TransactionController(ILogger<TransactionController> logger, APIDbContext dbContext)
        {
            this.logger = logger;
            transactionsRepository = new TransactionDataRepository(dbContext);
            accountsRepository = new AccountsRepository(dbContext);
        }


        [HttpGet("TransactionsHistory")]
        [ProducesResponseType(typeof(IEnumerable), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IEnumerable<TransactionData> GetTransactionsHistory()
        {
            var trans = transactionsRepository.GetUserTransactions(Global.TestUserId, true);
            return trans;
        }

        [HttpGet("TransactionsFiltered")]
        [ProducesResponseType(typeof(IEnumerable), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IEnumerable<TransactionData> GetTransactionsFiltered(TransactionType? type, DateTime? from, DateTime? to)
        {
            var trans = transactionsRepository.GetUserTransactionsFiltered(Global.TestUserId, type, from, to);
            return trans;
        }

        [HttpPost("CreateTransfer")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public string CreateTransfer(int accountNumberFrom, int accountNumberTo, decimal amount, string comment)
        {
            // TODO: validation

            // TODO: accountRepository - possible to transfer

            bool transfer_result = transactionsRepository.CreateTransfer(Global.TestUserId, accountNumberFrom, accountNumberTo, amount, comment);

            if (!transfer_result)
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
            var walletAccount = accountsRepository.GetWalletByUser(Global.TestUserId);
            bool transfer_result = transactionsRepository.CreateTransfer(Global.TestUserId, walletAccount.AccountNumber, targetAccountNumber, amount, "deposit");

            if (!transfer_result)
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
            var walletAccount = accountsRepository.GetWalletByUser(Global.TestUserId);

            bool transfer_result = transactionsRepository.CreateTransfer(Global.TestUserId, fromAccountNumber, walletAccount.AccountNumber, amount, "withdrawal");
            if (!transfer_result)
            {
                return "Transaction error";
            }

            return "Successfully created transaction";
        }

    }

}