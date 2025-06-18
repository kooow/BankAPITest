using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BankAPITest;

/// <summary>
/// Program class for the BankAPITest application.
/// </summary>
public class Program
{
    /// <summary>
    /// Main method for the application.
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        CreateHostBuilder(args)
            .Build()
            .Run();
    }

    /// <summary>
    /// Creates a host builder for the application.
    /// </summary>
    /// <param name="args">arguments</param>
    /// <returns>Host builder</returns>
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
    }
}
