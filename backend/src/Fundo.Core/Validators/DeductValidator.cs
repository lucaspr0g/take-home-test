using Ardalis.Result;
using Fundo.Core.Entities;
using Fundo.Core.Helpers;
using Fundo.Core.Interfaces;

namespace Fundo.Core.Validators
{
	public sealed class DeductValidator : IDeductValidator
	{
		public Result<Loan> Validate(int id, decimal amount)
		{
			if (id < LoanConstansHelper.LowestLoanId)
				return Result<Loan>.Invalid(new ValidationError(LoanConstansHelper.InvalidLoanId));

			if (!IsValidAmount(amount))
				return Result<Loan>.Invalid(new ValidationError($"Amount must be at least {LoanConstansHelper.LowestAcceptedValue}."));

			return Result.Success();
		}

		private static bool IsValidAmount(decimal amount)
		{
			return amount >= LoanConstansHelper.LowestAcceptedValue;
		}
	}
}