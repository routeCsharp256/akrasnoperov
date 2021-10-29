﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OzonEdu.MerchandiseService.Infrastructure.Middlewares
{
	/// <summary>
	/// Терминальный middleware для проверки того, что приложение поднялось.
	/// </summary>
	public class ReadyMiddleware
	{
		/// <summary>
		/// .ctor
		/// </summary>
		/// <param name="next">Следующий <see cref="RequestDelegate"/>.</param>
		public ReadyMiddleware(RequestDelegate next)
		{
		}

		/// <summary>
		/// Возвращает пустой ответ 200 OK.
		/// </summary>
		/// <param name="context"><see cref="HttpContext"/>.</param>
		public async Task InvokeAsync(HttpContext context)
		{
			context.Response.Clear();
			context.Response.StatusCode = StatusCodes.Status200OK;
			await context.Response.WriteAsync("200 Ok");
		}
	}
}