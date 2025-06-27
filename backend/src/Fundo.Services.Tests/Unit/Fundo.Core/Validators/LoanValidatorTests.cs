using Ardalis.Result;
using FluentAssertions;
using Fundo.Core.Interfaces;
using Fundo.Core.Validators;
using System.Linq;
using Xunit;

namespace Fundo.Services.Tests.Unit.Fundo.Core.Validators
{
	public class LoanValidatorTests
	{
		private readonly ILoanValidator _loanValidator;

		public LoanValidatorTests()
		{
			_loanValidator = new LoanValidator();
		}

		[Fact]
		public void ValidateCreate_ReturnsError_WhenInvalidAmount()
		{
			var amount = -1;
			var applicantName = "Lucas";

			var result = _loanValidator.ValidateCreate(amount, applicantName);

			result.Status.Should().Be(ResultStatus.Invalid);
			result.ValidationErrors.Should().HaveCount(1);
			result.ValidationErrors.First().ErrorMessage.Should().Contain("Amount must be at least");
		}

		[Fact]
		public void ValidateCreate_ReturnsError_WhenInvalidApplicantName()
		{
			var amount = 10;
			var applicantName = string.Empty;

			var result = _loanValidator.ValidateCreate(amount, applicantName);

			result.Status.Should().Be(ResultStatus.Invalid);
			result.ValidationErrors.Should().HaveCount(1);
			result.ValidationErrors.First().ErrorMessage.Should().Be("Invalid Applicant name.");
		}

		[Fact]
		public void ValidateCreate_ReturnsSuccess()
		{
			var amount = 10;
			var applicantName = "Lucas";

			var result = _loanValidator.ValidateCreate(amount, applicantName);

			result.Status.Should().Be(ResultStatus.Ok);
			result.ValidationErrors.Should().BeNullOrEmpty();
		}
	}
}