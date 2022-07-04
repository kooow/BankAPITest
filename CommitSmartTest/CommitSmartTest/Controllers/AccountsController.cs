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
using System.Threading.Tasks;

namespace CommitSmartTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public class AccountsController : ControllerBase
    {

        private readonly ILogger<AccountsController> logger;

        private readonly AccountsRepository accountsRepository;

        public AccountsController(ILogger<AccountsController> logger, APIDbContext dbContext)
        {
            this.logger = logger;
            accountsRepository = new AccountsRepository(dbContext);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        public IEnumerable<Account> GetAccounts()
        {
            var accounts = accountsRepository.GetAccountsByUser(Global.TestUserId, true);
            return accounts;
        }

    }
}
