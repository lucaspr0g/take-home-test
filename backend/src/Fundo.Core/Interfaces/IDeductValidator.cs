using Ardalis.Result;
using Fundo.Core.Entities;

namespace Fundo.Core.Interfaces
{
	public interface IDeductValidator
	{
		Result<Loan> Validate(int id, decimal amount);
	}
}