using Fundo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fundo.Infrastructure.Data.Configuration.Entities
{
	public sealed class LoanConfiguration : IEntityTypeConfiguration<Loan>
	{
		public void Configure(EntityTypeBuilder<Loan> builder)
		{
			builder.ToTable("Loan");

			builder.HasKey(l => l.Id);

			builder.Property(l => l.Id)
				.ValueGeneratedOnAdd();

			builder.Property(l => l.Amount)
				.HasPrecision(12, 2);

			builder.Property(l => l.CurrentBalance)
				.HasPrecision(12, 2);

			builder.Property(l => l.ApplicantName)
				.HasMaxLength(100);

			builder.Property(l => l.Status)
				.HasMaxLength(6);
		}
	}
}