namespace Fundo.Applications.UseCases.DTOs
{
	public sealed record GenerateTokenDto(string Issuer, string Audience, string SecretKey, DateTime Expires);
}