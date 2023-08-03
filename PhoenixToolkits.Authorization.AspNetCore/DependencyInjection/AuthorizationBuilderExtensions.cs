using Microsoft.AspNetCore.Authorization;
using Valhalla.Authorization.AspNetCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class AuthorizationBuilderExtensions
{
	public static AuthorizationBuilder AddHttpFallbackPolicy(this AuthorizationBuilder builder)
		=> builder.AddFallbackPolicy(
			"HttpFallback",
			new AuthorizationPolicyBuilder()
				.AddRequirements(new HttpAuthorizationRequirement())
				.Build());
}
