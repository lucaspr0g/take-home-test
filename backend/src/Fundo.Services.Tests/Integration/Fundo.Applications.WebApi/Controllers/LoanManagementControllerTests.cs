using Ardalis.Result;
using FluentAssertions;
using Fundo.Applications.UseCases.Commands.CreateLoan;
using Fundo.Applications.UseCases.Commands.CreateToken;
using Fundo.Applications.UseCases.Commands.DeductLoanBalance;
using Fundo.Applications.UseCases.DTOs;
using Fundo.Core.Entities;
using Fundo.Services.Tests.Integration.Configuration;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Fundo.Services.Tests.Integration
{
    public class LoanManagementControllerTests : IClassFixture<ApplicationFactory>
    {
        private readonly HttpClient _client;
		private readonly JsonSerializerOptions _serializerOptions;

		private string _token { get; set; }

		public LoanManagementControllerTests(ApplicationFactory factory)
        {
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

			_serializerOptions = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			};
		}

		[Fact]
		public async Task GetAll_ReturnsOk()
		{
			await SetToken();

			var response = await _client.GetAsync("/loans");

			var content = await response.Content.ReadAsStringAsync();
			var loans = JsonSerializer.Deserialize<IEnumerable<LoanDto>>(content, _serializerOptions);

			response.StatusCode.Should().Be(HttpStatusCode.OK);
			loans.Should().HaveCount(2);
		}

		[Fact]
		public async Task Get_ReturnsOk()
		{
			await SetToken();

			var response = await _client.GetAsync("/loans/1");

			var content = await response.Content.ReadAsStringAsync();
			var loan = JsonSerializer.Deserialize<LoanDto>(content, _serializerOptions);

			response.StatusCode.Should().Be(HttpStatusCode.OK);
			loan.Should().NotBeNull();
			loan.Id.Should().Be(1);
		}

		[Fact]
		public async Task Get_ReturnsNoContent()
		{
			await SetToken();

			var response = await _client.GetAsync("/loans/10");

			response.StatusCode.Should().Be(HttpStatusCode.NoContent);
		}

		[Fact]
		public async Task Create_ReturnsCreated()
		{
			await SetToken();

			var command = new CreateLoanCommand(1000, "Lucas");

			var response = await _client.PostAsJsonAsync("/loans", command);

			var content = await response.Content.ReadAsStringAsync();
			var result = JsonSerializer.Deserialize<Result<Loan>>(content, _serializerOptions);
			var loan = result.Value;

			response.StatusCode.Should().Be(HttpStatusCode.Created);
			result.Should().NotBeNull();
			loan.Should().NotBeNull();
			loan.Id.Should().Be(3);
		}

		[Fact]
		public async Task Create_ReturnsBadRequest_WhenInvalidAmount()
		{
			await SetToken();

			var command = new CreateLoanCommand(0, "Lucas");

			var response = await _client.PostAsJsonAsync("/loans", command);

			var content = await response.Content.ReadAsStringAsync();
			var result = JsonSerializer.Deserialize<Result>(content, _serializerOptions);
			var loan = result.Value;

			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
			loan.Should().BeNull();
			result.Should().NotBeNull();
			result.ValidationErrors.Should().HaveCount(1);
		}

		[Fact]
		public async Task Create_ReturnsBadRequest_WhenInvalidName()
		{
			await SetToken();

			var command = new CreateLoanCommand(2, string.Empty);

			var response = await _client.PostAsJsonAsync("/loans", command);

			var content = await response.Content.ReadAsStringAsync();
			var result = JsonSerializer.Deserialize<Result>(content, _serializerOptions);
			var loan = result.Value;

			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
			loan.Should().BeNull();
			result.Should().NotBeNull();
			result.ValidationErrors.Should().HaveCount(1);
		}

		[Fact]
		public async Task DeductBalance_ReturnsNotFound_WhenInvalidId()
		{
			await SetToken();

			var command = new DeductLoanBalanceCommand
			{
				Amount = 100
			};

			var response = await _client.PostAsJsonAsync("/loans/99/payment", command);

			var content = await response.Content.ReadAsStringAsync();
			var result = JsonSerializer.Deserialize<Result>(content, _serializerOptions);

			response.StatusCode.Should().Be(HttpStatusCode.NotFound);
			result.Errors.Should().HaveCount(1);
		}

		[Fact]
		public async Task DeductBalance_ReturnsOk()
		{
			await SetToken();

			var command = new DeductLoanBalanceCommand
			{
				Amount = 5
			};

			var response = await _client.PostAsJsonAsync("/loans/1/payment", command);

			var content = await response.Content.ReadAsStringAsync();
			var result = JsonSerializer.Deserialize<Result<Loan>>(content, _serializerOptions);
			var loan = result.Value;

			response.StatusCode.Should().Be(HttpStatusCode.OK);
			result.Errors.Should().BeNullOrEmpty();
			result.ValidationErrors.Should().BeNullOrEmpty();
			loan.Should().NotBeNull();
			loan.CurrentBalance.Should().Be(995);
		}

		private async Task SetToken()
		{
			var token = await GetToken();
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		}

		private async Task<string> GetToken()
		{
			if (_token is not null)
				return _token;

			_client.DefaultRequestHeaders.Add("ClientId", "9d2b1b34f3679601737cc7e3eddaf01f");
			var result = await _client.PostAsync("/auth", null);

			if (!result.IsSuccessStatusCode)
			{
				throw new Exception("Failed to retrieve token");
			}

			var content = await result.Content.ReadAsStringAsync();
			var response = JsonSerializer.Deserialize<CreateTokenCommandResponse>(content, _serializerOptions);

			_token = response!.Token;
			return _token;
		}
	}
}
