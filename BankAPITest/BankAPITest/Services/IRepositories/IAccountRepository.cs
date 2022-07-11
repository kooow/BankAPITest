using BankAPITest.Models;
using BankAPITest.Services.IRepositories;
using System.Collections.Generic;

namespace BankAPITest.Services.IRepositories
{
    public interface IAccountRepository : IRepository<Account>
    {
    }

}