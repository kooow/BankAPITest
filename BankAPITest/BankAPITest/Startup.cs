using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BankAPITest.Services;
using BankAPITest.Entities;

namespace BankAPITest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
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
                        Email = "kovacsrobert@windowslive.com"
                    },
                });

            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BankTest API V1");

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
                Name = "wallet",
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
                AccountType = (int) AccountTypes.BankAccount,
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

            var transaction1_account1 = new TransactionData()
            {
                AccountNumber = account1.AccountNumber,
                Date = DateTime.Now.AddDays(-1),
                TransactionType = TransactionType.Deposit.ToString(),
                Amount = 100,
                CurrentBalance = 100,
                Comment = "starting deposit",
            };

            context.Transactions.Add(transaction1_account1);
            context.SaveChanges();

        }

    }
}
