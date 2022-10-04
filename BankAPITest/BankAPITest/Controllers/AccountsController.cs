using BankAPITest.Entities;
using BankAPITest.Services.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;

namespace BankAPITest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public class AccountsController : ControllerBase
    {

        private readonly ILogger<AccountsController> _logger;

        private readonly IAccountsRepository _accountsRepository;

        public AccountsController(ILogger<AccountsController> logger, IAccountsRepository accountsRepository)
        {
            this._logger = logger;
            this._accountsRepository = accountsRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IEnumerable<Account> GetAccounts()
        {
            var accounts = _accountsRepository.GetAccountsByUser(Global.TestUserId, true);
            return accounts;
        }
    }
}
