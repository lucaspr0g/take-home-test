using Ardalis.Result;
using Fundo.Core.Entities;
using Fundo.Core.Interfaces;
using MediatR;

namespace Fundo.Applications.UseCases.Commands.CreateLoan
{
	public sealed class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, Result<Loan>>
	{
		private readonly ILoanService _loanService;

		public CreateLoanCommandHandler(ILoanService loanService)
		{
			_loanService = loanService;
		}

		public async Task<Result<Loan>> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
		{
			return await _loanService
				.CreateLoanAsync(request.Amount, request.ApplicantName, cancellationToken);
		}
	}
}