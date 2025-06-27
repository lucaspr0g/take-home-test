using Fundo.Applications.UseCases.DTOs;
using Fundo.Core.Entities;

namespace Fundo.Applications.UseCases.Extensions
{
	internal static class LoanMapper
	{
		internal static LoanDto? ToDto(this Loan? loan)
		{
			if (loan is null)
				return null;

			return new LoanDto
			{
				Id = loan.Id,
				LoanAmount = loan.Amount,
				Status = loan.Status,
				Applicant = loan.ApplicantName,
				CurrentBalance = loan.CurrentBalance
			};
		}

		internal static IEnumerable<LoanDto>? ToDto(this IEnumerable<Loan>? loans)
		{
			if (loans is null || !loans.Any())
				return null;

			return loans.Select(loan => ToDto(loan))!;
		}
	}
}