namespace Fundo.Applications.UseCases.DTOs
{
	public record LoanDto
	{
		public int Id { get; init; }

		public decimal LoanAmount { get; init; }

		public decimal CurrentBalance { get; init; }

		public string Applicant { get; init; }

		public string Status { get; init; }
	}
}