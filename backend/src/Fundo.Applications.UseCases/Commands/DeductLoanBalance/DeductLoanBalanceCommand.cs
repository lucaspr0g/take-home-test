using Ardalis.Result;
using Fundo.Core.Entities;
using MediatR;

namespace Fundo.Applications.UseCases.Commands.DeductLoanBalance
{
	public sealed class DeductLoanBalanceCommand : IRequest<Result<Loan>>
	{
		public int Id { get; set; }

		public decimal Amount { get; set; }
	}
}