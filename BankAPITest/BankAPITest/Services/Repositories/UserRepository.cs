using BankAPITest.Entities;

namespace BankAPITest.Services.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(APIDbContext context) : base(context) { }
    }

}