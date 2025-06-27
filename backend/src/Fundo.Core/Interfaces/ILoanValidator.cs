using Ardalis.Result;
using Fundo.Core.Entities;

namespace Fundo.Core.Interfaces
{
	public interface ILoanValidator
	{
		Result<Loan> ValidateCreate(decimal amount, string applicantName);
	}
}