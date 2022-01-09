using DomesticWarehousemanWebApi.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomesticWarehousemanWebApi.Middleware
{
	public class ErrorHandlingMiddleware : IMiddleware
	{
		private readonly ILogger<ErrorHandlingMiddleware> _logger;

		public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
		{
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next.Invoke(context);
			}
			catch (BadRequestException badRequestException)
			{
				context.Response.StatusCode = 400;
				await context.Response.WriteAsync(badRequestException.Message);
			}
			catch (ForbiddenException forbidException)
			{
				context.Response.StatusCode = 403;
				await context.Response.WriteAsync(forbidException.Message);
			}
			catch (Exception e)
			{
				_logger.LogError(e, e.Message);

				context.Response.StatusCode = 500;
				await context.Response.WriteAsync("Unspecified error has occured");
			}
		}
	}
}
