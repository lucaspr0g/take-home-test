using Ardalis.Result;
using MediatR;

namespace Fundo.Applications.UseCases.Commands.CreateToken
{
	public sealed record CreateTokenCommand(string ClientId) : IRequest<Result<CreateTokenCommandResponse>>;
}