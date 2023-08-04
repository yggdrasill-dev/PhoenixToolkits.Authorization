namespace Valhalla.Authorization.AspNetCore;

internal class MatchFunctionMiddleware : IMiddleware
{
	private readonly IAuthorizationSystem m_System;

	public MatchFunctionMiddleware(IAuthorizationSystem system)
	{
		m_System = system ?? throw new ArgumentNullException(nameof(system));
	}

	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		var functionMatcher = new HttpFunctionMatcher(context);

		var function = await m_System.GetFunctionAsync(
			functionMatcher,
			context.RequestAborted).ConfigureAwait(false);

		if (function is not null)
			context.Features.Set(function);

		await next(context).ConfigureAwait(false);
	}
}
