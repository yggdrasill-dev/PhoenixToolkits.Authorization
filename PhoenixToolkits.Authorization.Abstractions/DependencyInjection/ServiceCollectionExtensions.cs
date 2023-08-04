using Valhalla.Authorization;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static AuthorizationSystemBuilder AddPhoenixAuthorizationKits(
		this IServiceCollection services,
		string systemName)
	{
		_ = services.AddSingleton<IAuthorizationSystem>(
			sp => ActivatorUtilities.CreateInstance<AuthorizationSystem>(
				sp,
				systemName));

		return new AuthorizationSystemBuilder(services);
	}
}
