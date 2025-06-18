using BankAPITest.Entities;

namespace BankAPITest.Services.Repositories;

/// <summary>
/// Repository for managing users
/// </summary>
public class UserRepository : Repository<User>
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context">Database context</param>
    public UserRepository(APIDbContext context) : base(context) { }
}