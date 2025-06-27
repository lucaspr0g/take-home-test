using Ardalis.Result;
using FluentAssertions;
using Fundo.Core.Interfaces;
using Fundo.Core.Validators;
using System.Linq;
using Xunit;

namespace Fundo.Services.Tests.Unit.Fundo.Core.Validators
{
	public class DeductValidatorTests
	{
		private readonly IDeductValidator _deductValidator;

		public DeductValidatorTests()
		{
			_deductValidator = new DeductValidator();
		}

		[Fact]
		public void Validate_ReturnsError_WhenInvalidAmount()
		{
			var id = 1;
			var amount = 0;

			var result = _deductValidator.Validate(id, amount);

			result.Status.Should().Be(ResultStatus.Invalid);
			result.ValidationErrors.Should().HaveCount(1);
			result.ValidationErrors.First().ErrorMessage.Should().Contain("Amount must be at least");
		}

		[Fact]
		public void Validate_ReturnsError_WhenInvalidId()
		{
			var id = 0;
			var amount = 0;

			var result = _deductValidator.Validate(id, amount);

			result.Status.Should().Be(ResultStatus.Invalid);
			result.ValidationErrors.Should().HaveCount(1);
			result.ValidationErrors.First().ErrorMessage.Should().Contain("Invalid loan id");
		}

		[Fact]
		public void Validate_ReturnsSuccess()
		{
			var id = 1;
			var amount = 5;

			var result = _deductValidator.Validate(id, amount);

			result.Status.Should().Be(ResultStatus.Ok);
			result.ValidationErrors.Should().BeNullOrEmpty();
		}
	}
}