using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Fundo.Applications.WebApi.Middlewares
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _logger;

		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (SqlException ex)
			{
				_logger.LogError(ex, "Unexpected exception related to database");
				context.Response.StatusCode = 500;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected exception");
				context.Response.StatusCode = 500;
			}
		}
	}
}
