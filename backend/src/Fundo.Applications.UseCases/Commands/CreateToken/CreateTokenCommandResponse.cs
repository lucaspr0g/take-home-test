namespace Fundo.Applications.UseCases.Commands.CreateToken
{
	public sealed record CreateTokenCommandResponse(string Token, DateTime Expires);
}