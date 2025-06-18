using BankAPITest.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankAPITest.Services;

/// <summary>
/// Database context for the API
/// </summary>
public class APIDbContext : DbContext
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="options">Database context options</param>
    public APIDbContext(DbContextOptions<APIDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Model creating method to configure the database schema
    /// </summary>
    /// <param name="modelBuilder">Model builder</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Users in database
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// Accounts in database
    /// </summary>
    public DbSet<Account> Accounts { get; set; }

    /// <summary>
    /// Transactions in database
    /// </summary>
    public DbSet<TransactionData> Transactions { get; set; }
}
