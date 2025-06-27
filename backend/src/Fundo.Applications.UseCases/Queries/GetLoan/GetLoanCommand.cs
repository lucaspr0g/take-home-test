using Fundo.Applications.UseCases.DTOs;
using MediatR;

namespace Fundo.Applications.UseCases.Queries.GetLoan
{
	public sealed record GetLoanCommand(int Id) : IRequest<LoanDto?>;
}