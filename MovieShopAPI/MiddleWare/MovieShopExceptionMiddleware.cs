using ApplicationCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieShopAPI.MiddleWare
{
	// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
	public class MovieShopExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<MovieShopExceptionMiddleware> _logger;

		public MovieShopExceptionMiddleware(RequestDelegate next, ILogger<MovieShopExceptionMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			//read info from the httpcontext object and log it somewhere
			try
			{
				_logger.LogInformation("inside the middleware");
				await _next(httpContext);

			}
			catch (Exception ex)
			{

				var exceptionDetails = new ErrorModel
				{
					Message = ex.Message, 
					//StackTrace = ex.StackTrace, 
					//don't wanna show stack trace to the user
					ExceptionDateTime = DateTime.UtcNow, 
					ExceptionType = ex.GetType().ToString (), 
					Path = httpContext.Request.Path, 
					HttpRequestType = httpContext.Request.Method, 
					User = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity.Name : null
				};

				_logger.LogError("Exception happened, log this to text or json files using SeriLog");

				//return http status code 500
				httpContext.Response.ContentType = "application/json";
				httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				var result = JsonSerializer.Serialize<ErrorModel>(exceptionDetails);
				await httpContext.Response.WriteAsync(result);
			}

			//first catch the exception
			//check the exception message and type
			//check where the exception happened
			//when it happened
			//which url and which http method (controller, action method)
			//for which user, if user is logged in
			
			//save all the info somewhere (text files, json files, or database)
			//System.IO to create text file
			//Asp.net core has buildin logging mechanism, (ILogger)which can be used by any 3rd party log provide
			//*SeriLog* and NLog
			//send Email to dev team when exception happens
		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline.
	public static class MovieShopExceptionMiddlewareExtensions
	{
		public static IApplicationBuilder UseMovieShopExceptionMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<MovieShopExceptionMiddleware>();
		}
	}
}
