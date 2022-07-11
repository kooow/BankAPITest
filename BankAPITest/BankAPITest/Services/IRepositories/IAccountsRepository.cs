using BankAPITest.Entities;
using BankAPITest.Services.IRepositories;
using System.Collections.Generic;

namespace BankAPITest.Services.IRepositories
{
    public interface IAccountsRepository : IRepository<Account>
    {
        IEnumerable<Account> GetAccountsByUser(int userId, bool filteredWallet);

        Account GetWalletByUser(int userId);
    }

}