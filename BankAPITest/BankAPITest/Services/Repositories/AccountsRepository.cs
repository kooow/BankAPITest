using System.Linq;
using BankAPITest.Entities;
using System.Collections.Generic;
using BankAPITest.Services.IRepositories;

namespace BankAPITest.Services.Repositories
{

    public class AccountsRepository : Repository<Account>, IAccountsRepository
    {

        public AccountsRepository(APIDbContext context) : base(context) { }

        public IEnumerable<Account> GetAccountsByUser(int userId, bool filteredWallet)
        {
            var apiDbContext = Context as APIDbContext;

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

        public Account GetWalletByUser(int userId)
        {
            var apiDbContext = Context as APIDbContext;

            var walletAccount =
                ( from ac in apiDbContext.Accounts
                where ac.User.Id.Equals(userId)
                && ac.AccountType == (int)AccountTypes.Wallet
                select ac).FirstOrDefault();

            return walletAccount;
        }

        // TODO: possible to transfer

    }

}