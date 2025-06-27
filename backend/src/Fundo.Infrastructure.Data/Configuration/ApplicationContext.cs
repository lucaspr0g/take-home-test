using Fundo.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fundo.Infrastructure.Data.Configuration
{
	public class ApplicationContext : DbContext
	{
		public DbSet<Loan> Loans { get; set; }

		public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options) { }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
		}
	}
}
