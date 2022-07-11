using BankAPITest.Entities;
using System.Collections.Generic;

namespace BankAPITest.Services.Repositories
{

    public class UserRepository : Repository<User>
    {
        public UserRepository(APIDbContext context) : base(context) 
        { 
        }



    }

}