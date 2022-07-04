using CommitSmartTest.Models;
using System.Collections.Generic;

namespace CommitSmartTest.Services.Repositories
{

    public class UserRepository : Repository<User>
    {
        public UserRepository(APIDbContext context) : base(context) { }

    }

}