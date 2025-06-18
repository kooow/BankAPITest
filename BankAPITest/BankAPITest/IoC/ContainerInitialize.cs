using BankAPITest.Services.IRepositories;
using BankAPITest.Services.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Initializes the dependency injection container for the application.
/// </summary>
public static class ContainerInitialize
{
    /// <summary>
    /// Initializes the dependency injection container with the necessary services and repositories.
    /// </summary>
    /// <param name="services">Services</param>
    /// <param name="config">Configuration</param>
    /// <param name="testInit">Is test initalization?</param>
    public static void Init(IServiceCollection services, IConfiguration config, bool testInit = false)
    {
        SetupRepositories(services);
    }

    /// <summary>
    /// Sets up the repositories in the dependency injection container.
    /// </summary>
    /// <param name="services">Services</param>
    private static void SetupRepositories(IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IAccountsRepository, AccountsRepository>();
        services.AddScoped<ITransactionDataRepository, TransactionDataRepository>();
    }
}
