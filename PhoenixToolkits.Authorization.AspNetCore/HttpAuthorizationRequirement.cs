using Microsoft.AspNetCore.Authorization;

namespace Valhalla.Authorization.AspNetCore;

internal class HttpAuthorizationRequirement : AuthorizationHandler<HttpAuthorizationRequirement>, IAuthorizationRequirement
{
	protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, HttpAuthorizationRequirement requirement)
	{
		if (context.Resource is HttpContext httpContext)
		{
			var function = httpContext.Features.Get<IFunction>();
			var system = httpContext.RequestServices.GetRequiredService<ISystem>();
			var idResolver = httpContext.RequestServices.GetRequiredService<IIdentityResolveProvider>();

			if (function is null)
			{
				context.Fail(new AuthorizationFailureReason(this, "Can't found function data."));

				return;
			}

			if (await system.HasPermissionAsync(
				function,
				idResolver,
				httpContext.RequestAborted).ConfigureAwait(false))
				context.Succeed(requirement);
			else
				context.Fail(new AuthorizationFailureReason(this, "Access deny!"));
		}
	}
}
