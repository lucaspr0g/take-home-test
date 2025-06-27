using Ardalis.Result;
using Fundo.Core.Entities;
using Fundo.Core.Interfaces;
using MediatR;

namespace Fundo.Applications.UseCases.Commands.DeductLoanBalance
{
	public sealed class DeductLoanBalanceCommandHandler : IRequestHandler<DeductLoanBalanceCommand, Result<Loan>>
	{
		private readonly ILoanService _loanService;

		public DeductLoanBalanceCommandHandler(ILoanService loanService)
		{
			_loanService = loanService;
		}

		public async Task<Result<Loan>> Handle(DeductLoanBalanceCommand request, CancellationToken cancellationToken)
		{
			return await _loanService
				.DeductBalanceAsync(request.Id, request.Amount, cancellationToken);
		}
	}
}