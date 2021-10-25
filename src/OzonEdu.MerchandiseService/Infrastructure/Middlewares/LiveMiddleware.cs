﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OzonEdu.MerchandiseService.Infrastructure.Middlewares
{
	/// <summary>
	/// Терминальный middleware для проверки того, что приложение доступно.
	/// </summary>
	public class LiveMiddleware
	{
		/// <summary>
		/// .ctor
		/// </summary>
		/// <param name="next">Следующий <see cref="RequestDelegate"/>.</param>
		public LiveMiddleware(RequestDelegate next)
		{
		}

		/// <summary>
		/// Возвращает пустой ответ 200 OK.
		/// </summary>
		/// <param name="context"><see cref="HttpContext"/>.</param>
		public Task InvokeAsync(HttpContext context)
		{
			context.Response.StatusCode = StatusCodes.Status200OK;
			context.Response.Clear();

			return Task.CompletedTask;
		}
	}
}