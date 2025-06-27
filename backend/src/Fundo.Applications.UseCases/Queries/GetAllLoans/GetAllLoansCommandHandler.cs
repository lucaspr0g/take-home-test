using Fundo.Applications.UseCases.DTOs;
using Fundo.Applications.UseCases.Extensions;
using Fundo.Core.Interfaces;
using MediatR;

namespace Fundo.Applications.UseCases.Queries.GetAllLoans
{
	public sealed class GetAllLoansCommandHandler : IRequestHandler<GetAllLoansCommand, IEnumerable<LoanDto>?>
	{
		private readonly ILoanRepository _loanRepository;

		public GetAllLoansCommandHandler(ILoanRepository loanRepository)
		{
			_loanRepository = loanRepository;
		}

		public async Task<IEnumerable<LoanDto>?> Handle(GetAllLoansCommand request, CancellationToken cancellationToken)
		{
			return (await _loanRepository
				.GetAllAsync(cancellationToken))
				.ToDto();
		}
	}
}