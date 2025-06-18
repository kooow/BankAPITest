using BankAPITest.Entities;
using BankAPITest.Services.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;

namespace BankAPITest.Controllers;

/// <summary>
/// Accounts controller for managing user accounts
/// </summary>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
public class AccountsController : ControllerBase
{
    private readonly ILogger<AccountsController> m_logger;
    private readonly IAccountsRepository m_accountsRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger">Logger</param>
    /// <param name="accountsRepository">Accounts repository</param>
    public AccountsController(ILogger<AccountsController> logger, IAccountsRepository accountsRepository)
    {
        m_logger = logger;
        m_accountsRepository = accountsRepository;
    }

    /// <summary>
    /// Get accounts for the test user
    /// </summary>
    /// <returns>Account list</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public IEnumerable<Account> GetAccounts()
    {
        return m_accountsRepository.GetAccountsByUser(Global.TestUserId, true);
    }
}
