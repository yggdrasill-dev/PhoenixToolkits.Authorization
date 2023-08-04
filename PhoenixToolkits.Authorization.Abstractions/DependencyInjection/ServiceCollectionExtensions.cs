using Valhalla.Authorization;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static AuthorizationSystemBuilder AddPhoenixAuthorizationKits(this IServiceCollection services)
	{
		services.AddSingleton<IAuthorizationSystem, AuthorizationSystem>();

		return new AuthorizationSystemBuilder(services);
	}
}
