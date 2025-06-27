using Fundo.Core.Entities;
using Fundo.Core.Interfaces;
using Fundo.Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Fundo.Infrastructure.Data.Repositories
{
	public sealed class LoanRepository : ILoanRepository
	{
		private readonly ApplicationContext _dbContext;

		public LoanRepository(ApplicationContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Loan> AddAsync(Loan entity, CancellationToken cancellationToken)
		{
			_dbContext.Loans.Add(entity);
			await _dbContext.SaveChangesAsync(cancellationToken);

			return entity;
		}

		public async Task<Loan?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken)
		{
			return await _dbContext.Loans
				.FirstOrDefaultAsync(loan => loan.Id.Equals(id), cancellationToken);
		}

		public async Task<IEnumerable<Loan>> GetAllAsync(CancellationToken cancellationToken)
		{
			return await _dbContext.Loans
				.ToListAsync(cancellationToken);
		}

		public async Task<Loan> UpdateAsync(Loan loan, CancellationToken cancellationToken)
		{
			_dbContext.Loans.Update(loan);
			await _dbContext.SaveChangesAsync(cancellationToken);

			return loan;
		}
	}
}