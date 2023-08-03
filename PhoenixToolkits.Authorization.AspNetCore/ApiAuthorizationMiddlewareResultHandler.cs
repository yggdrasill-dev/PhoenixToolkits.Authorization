using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace Valhalla.Authorization.AspNetCore;

internal class ApiAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
	public Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
	{
		return authorizeResult.Succeeded ? next(context) : Handle();

		async Task Handle()
		{
			if (authorizeResult.Challenged)
			{
				if (policy.AuthenticationSchemes.Count > 0)
				{
					foreach (var scheme in policy.AuthenticationSchemes)
					{
						await context.ChallengeAsync(scheme).ConfigureAwait(false);
					}
				}
				else
				{
					await context.ChallengeAsync().ConfigureAwait(false);
				}
			}
			else if (authorizeResult.Forbidden)
			{
				context.Response.StatusCode = 403;
			}
		}
	}
}
