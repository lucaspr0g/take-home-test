using Ardalis.Result;
using FluentAssertions;
using Fundo.Core.Entities;
using Fundo.Core.Interfaces;
using Fundo.Core.Services;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Fundo.Services.Tests.Unit.Fundo.Core.Services
{
	public class LoanServiceTests
	{
		private readonly Mock<ILoanRepository> _loanRepositoryMock;
		private readonly Mock<ILoanValidator> _loanValidatorMock;
		private readonly Mock<IDeductValidator> _deductValidatorMock;
		private readonly ILoanService _loanService;

		public LoanServiceTests()
		{
			_loanRepositoryMock = new Mock<ILoanRepository>();
			_loanValidatorMock = new Mock<ILoanValidator>();
			_deductValidatorMock = new Mock<IDeductValidator>();
			_loanService = new LoanService(_loanRepositoryMock.Object, _loanValidatorMock.Object, _deductValidatorMock.Object);
		}

		[Fact]
		public async Task CreateLoanAsync_ReturnsCreated_WhenValidData()
		{
			var amount = 1;
			var applicantName = "Lucas";
			var expectedLoan = new Loan 
			{
				Id = 1,
				ApplicantName = applicantName,
				CurrentBalance = amount,
				Status = "active",
				Amount = amount
			};

			_loanRepositoryMock
				.Setup(repo => repo.AddAsync(It.IsAny<Loan>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(expectedLoan);

			_loanValidatorMock
				.Setup(validator => validator.ValidateCreate(amount, applicantName))
				.Returns(Result.Success());

			var result = await _loanService.CreateLoanAsync(amount, applicantName, CancellationToken.None);

			result.Status.Should().Be(ResultStatus.Created);
			result.Value.Should().Be(expectedLoan);
			_loanRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Loan>(), It.IsAny<CancellationToken>()), Times.Once);
		}

		[Fact]
		public async Task DeductBalanceAsync_ReturnsError_WhenLoanNotFound()
		{
			var id = 1;
			var amount = 1;

			_loanRepositoryMock
				.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()));

			_deductValidatorMock
				.Setup(validator => validator.Validate(id, amount))
				.Returns(Result.Success());

			var result = await _loanService.DeductBalanceAsync(id, amount, CancellationToken.None);

			result.Status.Should().Be(ResultStatus.NotFound);
			_loanRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
			_loanRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Loan>(), It.IsAny<CancellationToken>()), Times.Never);
		}

		[Fact]
		public async Task DeductBalanceAsync_ReturnsError_WhenLoanAlreadyPaid()
		{
			var id = 1;
			var amount = 1;
			var paidLoan = new Loan
			{
				Status = "paid"
			};

			_loanRepositoryMock
				.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(paidLoan);

			_deductValidatorMock
				.Setup(validator => validator.Validate(id, amount))
				.Returns(Result.Success());

			var result = await _loanService.DeductBalanceAsync(id, amount, CancellationToken.None);

			result.Status.Should().Be(ResultStatus.Conflict);
			result.Errors.Should().HaveCount(1);
			result.Errors.First().Should().Be("Loan already paid.");
			_loanRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
			_loanRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Loan>(), It.IsAny<CancellationToken>()), Times.Never);
		}

		[Fact]
		public async Task DeductBalanceAsync_ReturnsError_WhenAmountGreaterThanBalance()
		{
			var id = 1;
			var amount = 1;
			var paidLoan = new Loan
			{
				Status = "active"
			};

			_loanRepositoryMock
				.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(paidLoan);

			_deductValidatorMock
				.Setup(validator => validator.Validate(id, amount))
				.Returns(Result.Success());

			var result = await _loanService.DeductBalanceAsync(id, amount, CancellationToken.None);

			result.Status.Should().Be(ResultStatus.Invalid);
			result.ValidationErrors.Should().HaveCount(1);
			result.ValidationErrors.First().ErrorMessage.Should().Be("Amount cannot be greater than the current balance.");
			_loanRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
			_loanRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Loan>(), It.IsAny<CancellationToken>()), Times.Never);
		}

		[Fact]
		public async Task DeductBalanceAsync_ReturnsSuccess_WhenValidValues()
		{
			var id = 1;
			var amount = 1;
			var paidLoan = new Loan
			{
				CurrentBalance = 2,
				Status = "active"
			};

			_loanRepositoryMock
				.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(paidLoan);

			_loanRepositoryMock
				.Setup(repo => repo.UpdateAsync(It.IsAny<Loan>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(paidLoan);

			_deductValidatorMock
				.Setup(validator => validator.Validate(id, amount))
				.Returns(Result.Success());

			var result = await _loanService.DeductBalanceAsync(id, amount, CancellationToken.None);

			result.Status.Should().Be(ResultStatus.Ok);
			result.Value.Status.Should().Be("active");
			_loanRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
			_loanRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Loan>(), It.IsAny<CancellationToken>()), Times.Once);
		}

		[Fact]
		public async Task DeductBalanceAsync_ReturnsSuccess_WhenValidValuesAndPaid()
		{
			var id = 1;
			var amount = 1;
			var paidLoan = new Loan
			{
				CurrentBalance = 1,
				Status = "active"
			};

			_loanRepositoryMock
				.Setup(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(paidLoan);

			_loanRepositoryMock
				.Setup(repo => repo.UpdateAsync(It.IsAny<Loan>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(paidLoan);

			_deductValidatorMock
				.Setup(validator => validator.Validate(id, amount))
				.Returns(Result.Success());

			var result = await _loanService.DeductBalanceAsync(id, amount, CancellationToken.None);

			result.Status.Should().Be(ResultStatus.Ok);
			result.Value.Status.Should().Be("paid");
			_loanRepositoryMock.Verify(repo => repo.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
			_loanRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Loan>(), It.IsAny<CancellationToken>()), Times.Once);
		}
	}
}