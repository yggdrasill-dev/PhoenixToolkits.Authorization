namespace Valhalla.Authorization.AspNetCore;

internal class MatchFunctionMiddleware(IAuthorizationSystem system) : IMiddleware
{
	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		var functionMatcher = new HttpFunctionMatcher(context);

		var function = await system.GetFunctionAsync(
			functionMatcher,
			context.RequestAborted).ConfigureAwait(false);

		if (function is not null)
			context.Features.Set(function);

		await next(context).ConfigureAwait(false);
	}
}
