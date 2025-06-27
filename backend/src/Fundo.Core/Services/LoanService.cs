using Ardalis.Result;
using Fundo.Core.Entities;
using Fundo.Core.Helpers;
using Fundo.Core.Interfaces;

namespace Fundo.Core.Services
{
	public sealed class LoanService : ILoanService
	{
		private readonly ILoanValidator _loanValidator;
		private readonly ILoanRepository _loanRepository;
		private readonly IDeductValidator _deductValidator;

		public LoanService(ILoanRepository loanRepository, ILoanValidator loanValidator, IDeductValidator deductValidator)
		{
			_loanValidator = loanValidator;
			_loanRepository = loanRepository;
			_deductValidator = deductValidator;
		}

		public async Task<Result<Loan>> CreateLoanAsync(decimal amount, string applicantName, CancellationToken cancellationToken)
		{
			var result = _loanValidator.ValidateCreate(amount, applicantName);
			if (!result.IsSuccess)
				return result;

			var loan = new Loan(amount, applicantName);
			return Result.Created(await _loanRepository.AddAsync(loan, cancellationToken));
		}

		public async Task<Result<Loan>> DeductBalanceAsync(int id, decimal amount, CancellationToken cancellationToken)
		{
			var result = _deductValidator.Validate(id, amount);
			if (!result.IsSuccess)
				return result;

			var loan = await _loanRepository.GetByIdAsync(id, cancellationToken);
			if (loan is null)
				return Result.NotFound(LoanConstansHelper.LoanNotFound);

			if (loan.Status == LoanStatusHelper.Paid)
				return Result.Conflict(LoanConstansHelper.LoanAlreadyPaid);

			if (loan.CurrentBalance - amount < 0)
				return Result.Invalid(new ValidationError(LoanConstansHelper.AmountGreaterThanBalance));

			loan.CurrentBalance -= amount;
			if (loan.CurrentBalance == 0)
				loan.Status = LoanStatusHelper.Paid;

			return Result.Success(await _loanRepository.UpdateAsync(loan, cancellationToken));
		}
	}
}