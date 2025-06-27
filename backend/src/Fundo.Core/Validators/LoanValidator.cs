using Ardalis.Result;
using Fundo.Core.Entities;
using Fundo.Core.Helpers;
using Fundo.Core.Interfaces;

namespace Fundo.Core.Validators
{
	public sealed class LoanValidator : ILoanValidator
	{
		public Result<Loan> ValidateCreate(decimal amount, string applicantName)
		{
			if (!IsValidAmount(amount))
				return Result<Loan>.Invalid(new ValidationError($"Amount must be at least {LoanConstansHelper.LowestAcceptedValue}."));

			if (string.IsNullOrWhiteSpace(applicantName))
				return Result<Loan>.Invalid(new ValidationError(LoanConstansHelper.InvalidApplicantName));

			return Result.Success();
		}

		private static bool IsValidAmount(decimal amount)
		{
			return amount >= LoanConstansHelper.LowestAcceptedValue;
		}
	}
}