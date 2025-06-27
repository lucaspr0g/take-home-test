using Ardalis.Result;
using Fundo.Applications.UseCases.DTOs;
using Fundo.Applications.UseCases.Extensions;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Fundo.Applications.UseCases.Commands.CreateToken
{
	public sealed class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, Result<CreateTokenCommandResponse>>
	{
		private readonly string _issuer;
		private readonly string _audience;
		private readonly string _secretKey;
		private readonly string _clientSecret;

		public CreateTokenCommandHandler(IConfiguration configuration)
		{
			_issuer = configuration["Jwt:Issuer"];
			_audience = configuration["Jwt:Audience"];
			_secretKey = configuration["Jwt:SecretKey"];
			_clientSecret = configuration["Jwt:ClientSecret"];
		}

		public async Task<Result<CreateTokenCommandResponse>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
		{
			if (string.IsNullOrWhiteSpace(request.ClientId) ||
				request.ClientId.Equals(_clientSecret, StringComparison.Ordinal))
				return Result.Unauthorized();

			var expires = DateTime.UtcNow.AddHours(1);
			var dto = new GenerateTokenDto(_issuer, _audience, _secretKey, expires);

			var token = TokenHelper.GenerateToken(dto);
			return await Task.FromResult(new CreateTokenCommandResponse(token, expires));
		}
	}
}