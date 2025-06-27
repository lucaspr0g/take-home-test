using Fundo.Applications.WebApi;
using Fundo.Core.Entities;
using Fundo.Infrastructure.Data.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Testcontainers.MsSql;
using Xunit;

namespace Fundo.Services.Tests.Integration.Configuration
{
	public class ApplicationFactory : WebApplicationFactory<Startup>, IAsyncLifetime
	{
		private readonly MsSqlContainer _dbContainer = new MsSqlBuilder()
			.WithImage("mcr.microsoft.com/mssql/server:2022-latest")
			.WithPassword("pwd123456@")
			.Build();

		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(services =>
			{
				services.AddDbContext<ApplicationContext>(options =>
				{
					options.UseSqlServer(GetConnectionString());
				});

				using var scope = services.BuildServiceProvider().CreateScope();
				var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

				db.Database.EnsureDeleted();
				db.Database.EnsureCreated();

				db.Loans.Add(new Loan
				{
					Amount = 1000,
					ApplicantName = "User1",
					Status = "active",
					CurrentBalance = 1000
				});

				var amount = GenerateRandomAmount();

				db.Loans.Add(new Loan
				{
					Amount = amount,
					ApplicantName = "User2",
					Status = "active",
					CurrentBalance = amount
				});

				db.SaveChanges();
			});
		}

		public Task InitializeAsync()
		{
			return _dbContainer.StartAsync();
		}

		public new Task DisposeAsync()
		{
			return _dbContainer.StopAsync();
		}

		private static decimal GenerateRandomAmount()
		{
			return new Random().Next(1, 2000);
		}

		private SqlConnection GetConnectionString()
		{
			var connection = new SqlConnectionStringBuilder(_dbContainer.GetConnectionString())
			{
				InitialCatalog = "DBTests"
			};

			return new SqlConnection(connection.ToString());
		}
	}
}