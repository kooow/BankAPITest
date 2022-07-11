using BankAPITest.Services.IRepositories;
using BankAPITest.Services.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ContainerInitialize
{
    public static void Init(IServiceCollection services, IConfiguration config, bool testInit = false)
    {
        SetupRepositories(services);
    }


    private static void SetupRepositories(IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        services.AddScoped<IAccountsRepository, AccountsRepository>();
        services.AddScoped<ITransactionDataRepository, TransactionDataRepository>();

    }

}
