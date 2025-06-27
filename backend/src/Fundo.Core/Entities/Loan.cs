namespace Fundo.Core.Entities
{
	public sealed class Loan
	{
		private const string ActiveStatus = "active";

		public int Id { get; set; }

		public decimal Amount { get; set; }

		public decimal CurrentBalance { get; set; }

		public string ApplicantName { get; set; }

		public string Status { get; set; }

		public Loan() { }

		public Loan(decimal amount, string applicantName)
		{
			Amount = CurrentBalance = amount;
			ApplicantName = applicantName;
			Status = ActiveStatus;
		}
	}
}