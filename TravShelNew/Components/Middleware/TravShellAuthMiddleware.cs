using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace TravShelNew.Components.Middleware
{
	//https://learn.microsoft.com/en-us/aspnet/core/blazor/security/server/additional-scenarios?view=aspnetcore-8.0
	public class TravShellAuthMiddleware
	{
		private readonly RequestDelegate next;

		public static IDictionary<Guid,ClaimsPrincipal> Logins = new ConcurrentDictionary<Guid,ClaimsPrincipal>();

		public TravShellAuthMiddleware(RequestDelegate next)
		{
			this.next = next ?? throw new ArgumentNullException(nameof(next));
		}

		public async Task InvokeAsync(HttpContext context)
		{
         //   Console.WriteLine("Middleware");
			if (context.Request.Path=="/login" && context.Request.Query.ContainsKey("key") )
				{
				//Console.WriteLine("Middleware true");
				var key = Guid.Parse(context.Request.Query["key"]);
				var claim = Logins[key];
				await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claim);
				context.Response.Redirect("/");
			}
			else {
				//Console.WriteLine("Middleware false");
				await next(context);
			}
		}
	}
}
