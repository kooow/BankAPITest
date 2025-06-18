using BankAPITest.Entities;
using BankAPITest.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace BankAPITest;

/// <summary>
/// Startup class for configuring services and the HTTP request pipeline.
/// </summary>
public class Startup
{
    private const string SwaggerEndpointName = "BankTest API V1";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configuration">Configuration</param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// Configuration 
    /// </summary>
    public IConfiguration Configuration { get; }

    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services">Service collection</param>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<APIDbContext>(opt => opt.UseInMemoryDatabase("BankTest"));

        ContainerInitialize.Init(services, Configuration);

        services.AddControllers();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "BankTest API",
                Version = "v1",
                Description = "Description for the API goes here.",
                Contact = new OpenApiContact
                {
                    Name = "Robert Kovacs",
                    Email = ""
                },
            });
        });
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app">Application builder</param>
    /// <param name="env">Web host environment</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", SwaggerEndpointName);

            // To serve SwaggerUI at application's root page, set the RoutePrefix property to an empty string.
            c.RoutePrefix = string.Empty;
        });

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true) // allow any origin
            .AllowCredentials()); // allow credentials

        app.UseAuthorization();

        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<APIDbContext>();
            if (context is null)
            {
                throw new InvalidOperationException($"{nameof(APIDbContext)} is not available.");
            }

            DatabaseSeeding(context);
        }

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

    }

    /// <summary>
    /// Seed the database.
    /// </summary>
    /// <param name="context"></param>
    private void DatabaseSeeding(APIDbContext context)
    {
        var testUser1 = new User
        {
            Id = Global.TestUserId,
            FirstName = "Luke",
            LastName = "Skywalker"
        };
        context.Users.Add(testUser1);
        context.SaveChanges();

        var accountWallet = new Account()
        {
            AccountNumber = 0,
            Name = nameof(AccountTypes.Wallet).ToLower(),
            AccountType = (int)AccountTypes.Wallet,
            Balance = 0,
            ModifyDate = DateTime.Now,
            User = testUser1,
        };
        context.Accounts.Add(accountWallet);
        context.SaveChanges();

        var account1 = new Account()
        {
            AccountNumber = 1,
            Name = "main account",
            AccountType = (int)AccountTypes.BankAccount,
            Balance = 100,
            ModifyDate = DateTime.Now,
            User = testUser1,
        };
        context.Accounts.Add(account1);
        context.SaveChanges();

        var account2 = new Account()
        {
            AccountNumber = 2,
            AccountType = (int)AccountTypes.BankAccount,
            Name = "savings",
            Balance = (decimal)2.2f,
            ModifyDate = DateTime.Now,
            User = testUser1,
        };
        context.Accounts.Add(account2);
        context.SaveChanges();

        var transaction1Account1 = new TransactionData()
        {
            AccountNumber = account1.AccountNumber,
            Date = DateTime.Now.AddDays(-1),
            TransactionType = nameof(TransactionType.Deposit),
            Amount = 100,
            CurrentBalance = 100,
            Comment = "starting deposit",
        };

        context.Transactions.Add(transaction1Account1);
        context.SaveChanges();
    }
}
