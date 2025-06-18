using BankAPITest.Entities;
using BankAPITest.Services.IRepositories;
using System.Collections.Generic;
using System.Linq;

namespace BankAPITest.Services.Repositories;

/// <summary>
/// Repository for managing accounts
/// </summary>
public class AccountsRepository : Repository<Account>, IAccountsRepository
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context">Database context</param>
    public AccountsRepository(APIDbContext context) : base(context)
    {
    }

    /// <inheritdoc/>
    public IEnumerable<Account> GetAccountsByUser(int userId, bool filteredWallet)
    {
        APIDbContext? apiDbContext = Context as APIDbContext;
        if (apiDbContext is null)
        {
            throw new System.InvalidOperationException($"Context is not of type {nameof(APIDbContext)}.");
        }

        IQueryable<Account> accounts = from ac in apiDbContext.Accounts
                                       where ac.User.Id.Equals(userId)
                                       select ac;

        // hidden for users
        if (filteredWallet)
        {
            accounts = accounts.Where(ac => ac.AccountType != (int)AccountTypes.Wallet);
        }

        return accounts.ToList();
    }

    /// <inheritdoc/>
    public Account GetWalletByUser(int userId)
    {
        var apiDbContext = Context as APIDbContext;

        var walletAccount =
            (from ac in apiDbContext.Accounts
             where ac.User.Id.Equals(userId)
             && ac.AccountType == (int)AccountTypes.Wallet
             select ac).FirstOrDefault();

        return walletAccount;
    }

    // TODO: possible to transfer

}