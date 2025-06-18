using BankAPITest.Entities;
using System.Collections.Generic;

namespace BankAPITest.Services.IRepositories;

/// <summary>
/// Interface for accounts repository
/// </summary>
public interface IAccountsRepository : IRepository<Account>
{
    /// <summary>
    /// Returns all accounts for a user
    /// </summary>
    /// <param name="userId">User id</param>
    /// <param name="filteredWallet">Is the wallet filtered</param>
    /// <returns></returns>
    IEnumerable<Account> GetAccountsByUser(int userId, bool filteredWallet);

    /// <summary>
    /// Returns a wallet account for a user
    /// </summary>
    /// <param name="userId">User Id</param>
    /// <returns>Account entity</returns>
    Account GetWalletByUser(int userId);
}