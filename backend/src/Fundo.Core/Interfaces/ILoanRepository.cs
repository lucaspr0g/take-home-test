using Fundo.Core.Entities;

namespace Fundo.Core.Interfaces
{
	public interface ILoanRepository : IRepository<Loan>
	{
		Task<Loan> UpdateAsync(Loan loan, CancellationToken cancellationToken);
	}
}