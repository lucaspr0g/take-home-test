using Ardalis.Result;
using Fundo.Core.Entities;
using MediatR;

namespace Fundo.Applications.UseCases.Commands.CreateLoan
{
	public sealed record CreateLoanCommand(decimal Amount, string ApplicantName) : IRequest<Result<Loan>>;
}