using Fundo.Applications.UseCases.DTOs;
using Fundo.Applications.UseCases.Extensions;
using Fundo.Core.Interfaces;
using MediatR;

namespace Fundo.Applications.UseCases.Queries.GetLoan
{
	public sealed class GetLoanCommandHandler : IRequestHandler<GetLoanCommand, LoanDto?>
	{
		private readonly ILoanRepository _loanRepository;

		public GetLoanCommandHandler(ILoanRepository loanRepository)
		{
			_loanRepository = loanRepository;
		}

		public async Task<LoanDto?> Handle(GetLoanCommand request, CancellationToken cancellationToken)
		{
			return (await _loanRepository
				.GetByIdAsync(request.Id, cancellationToken))
				.ToDto();
		}
	} 
}