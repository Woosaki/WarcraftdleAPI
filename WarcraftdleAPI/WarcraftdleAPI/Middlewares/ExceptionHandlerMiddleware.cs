﻿using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using WarcraftdleAPI.Domain.Exceptions;

namespace WarcraftdleAPI.Middlewares;

public class ExceptionHandlerMiddleware(RequestDelegate next)
{
	public async Task Invoke(HttpContext context)
	{
		try
		{
			await next(context);
		}
		catch (Exception exception)
		{
			if (exception is ApiException apiException)
			{
				context.Response.ContentType = "application/json";
				context.Response.StatusCode = (int)apiException.StatusCode;

				var errorJson = $"{{\"error\":\"{apiException.Message}\",\"status\":{(int)apiException.StatusCode}}}";
				await context.Response.WriteAsync(errorJson);
			}
			else
			{
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			}
		}
	}
}
