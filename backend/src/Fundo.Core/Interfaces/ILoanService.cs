using Ardalis.Result;
using Fundo.Core.Entities;

namespace Fundo.Core.Interfaces
{
	public interface ILoanService
	{
		Task<Result<Loan>> CreateLoanAsync(decimal amount, string applicantName, CancellationToken cancellationToken);
		Task<Result<Loan>> DeductBalanceAsync(int id, decimal amount, CancellationToken cancellationToken);
	}
}