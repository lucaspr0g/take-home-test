using Ardalis.Result;
using Fundo.Applications.UseCases.Commands.CreateLoan;
using Fundo.Applications.UseCases.Commands.DeductLoanBalance;
using Fundo.Applications.UseCases.Queries.GetAllLoans;
using Fundo.Applications.UseCases.Queries.GetLoan;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Fundo.Applications.WebApi.Controllers
{
	[Authorize]
	[Route("/loans")]
    public class LoanManagementController : Controller
    {
		private readonly IMediator _mediator;

		public LoanManagementController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("{id}")]
		public async Task<ActionResult> Get([FromRoute] GetLoanCommand request, CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(request, cancellationToken);
			return Ok(result);
		}

		[HttpGet]
		public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new GetAllLoansCommand(), cancellationToken);
			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult> Create([FromBody] CreateLoanCommand request, CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(request, cancellationToken);
			return HandleResult(result);
		}

		[HttpPost("{id}/payment")]
		public async Task<ActionResult> DeductBalance(
			[FromRoute] int id,
			[FromBody] DeductLoanBalanceCommand request,
			CancellationToken cancellationToken
		)
		{
			request.Id = id;
			var result = await _mediator.Send(request, cancellationToken);

			return HandleResult(result);
		}

		private static ActionResult HandleResult<T>(Result<T> result)
		{
			return result.Status switch
			{
				ResultStatus.Ok => new OkObjectResult(result),
				ResultStatus.Invalid => new BadRequestObjectResult(result),
				ResultStatus.NotFound => new NotFoundObjectResult(result),
				ResultStatus.Created => new CreatedResult(nameof(Create), result),
				ResultStatus.Conflict => new ConflictObjectResult(result),
				_ => new StatusCodeResult(StatusCodes.Status500InternalServerError)
			};
		}
	}
}