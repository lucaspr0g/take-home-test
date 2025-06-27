using Fundo.Applications.UseCases.DTOs;
using MediatR;

namespace Fundo.Applications.UseCases.Queries.GetAllLoans
{
	public sealed record GetAllLoansCommand : IRequest<IEnumerable<LoanDto>?>;
}