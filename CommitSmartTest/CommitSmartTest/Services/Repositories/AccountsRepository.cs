﻿using System;
using System.Linq;
using System.Linq.Expressions;
using CommitSmartTest.Services;
using CommitSmartTest.Models;
using System.Collections.Generic;

namespace CommitSmartTest.Services.Repositories
{

    public class AccountsRepository : Repository<Account>
    {

        public AccountsRepository(APIDbContext context) : base(context) 
        { 
        }

        public IEnumerable<Account> GetAccountsByUser(string userId, bool filteredWallet)
        {
            var apiDbContext = Context as APIDbContext;

            IQueryable<Account> accounts = from ac in apiDbContext.Accounts
                           where ac.User.UserId.Equals(userId)
                         
                           select ac;

            // hidden for users
            if (filteredWallet)
            {    
                accounts = accounts.Where(ac => ac.AccountType != (int)AccountTypes.Wallet);
            }

            return accounts.ToList();
        }

        public Account GetWalletByUser(string userId)
        {
            var apiDbContext = Context as APIDbContext;

            var walletAccount =
                ( from ac in apiDbContext.Accounts
                where ac.User.UserId.Equals(userId)
                && ac.AccountType == (int)AccountTypes.Wallet
                select ac).FirstOrDefault();

            return walletAccount;
        }

        // TODO: possible to transfer

    }

}