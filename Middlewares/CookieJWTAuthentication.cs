namespace ProfitTest_Cafeteria.API.Middlewares
{
	public class CookieJWTAuthentication
	{
		private readonly RequestDelegate _next;

		public CookieJWTAuthentication(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			var token = context.Request.Cookies[AuthOptions.PARAMNAME];
			if (!string.IsNullOrEmpty(token))
			{
				context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
				context.Response.Headers.Add("X-Xss-Protection", "1");
				context.Response.Headers.Add("X-Frame-Options", "DENY");
				context.Request.Headers.Add("Authorization", "Bearer " + token);
			}

			await _next.Invoke(context);
		}
	}
}
