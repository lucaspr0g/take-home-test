using Fundo.Applications.UseCases.Commands.CreateToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Fundo.Applications.WebApi.Controllers
{
	[Route("/auth")]
	public class AuthorizationController : ControllerBase
	{
		private readonly IMediator _mediator;

		public AuthorizationController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> CreateToken(
			[FromHeader] string clientId, CancellationToken cancellationToken)
		{
			var command = new CreateTokenCommand(clientId);
			var result = await _mediator.Send(command, cancellationToken);

			if (!result.IsSuccess)
				return Unauthorized();

			return CreatedAtAction(nameof(CreateToken), result.Value);	
		}
	}
}