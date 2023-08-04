using Microsoft.AspNetCore.Authorization;
using Valhalla.Authorization.AspNetCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddWebApiAuthorizationMiddlewareResultHandler(this IServiceCollection services)
		=> services
			.AddTransient<IAuthorizationMiddlewareResultHandler, ApiAuthorizationMiddlewareResultHandler>()
			.AddSingleton<MatchFunctionMiddleware>();
}
