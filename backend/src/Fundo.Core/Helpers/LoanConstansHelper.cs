namespace Fundo.Core.Helpers
{
	public static class LoanConstansHelper
	{
		public const int LowestLoanId = 1;
		public const int LowestAcceptedValue = 1;

		public const string LoanNotFound = "Loan not found.";
		public const string InvalidLoanId = "Invalid loan id.";
		public const string LoanAlreadyPaid = "Loan already paid.";
		public const string InvalidApplicantName = "Invalid Applicant name.";
		public const string AmountGreaterThanBalance = "Amount cannot be greater than the current balance.";
	}
}