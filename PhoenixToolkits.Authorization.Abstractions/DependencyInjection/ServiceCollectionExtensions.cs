using Valhalla.Authorization;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddPhoenixAuthorizationKits(this IServiceCollection services)
		=> services
			.AddSingleton<ISystem, AuthorizationSystem>();

	public static IServiceCollection RegisterIdentityResolveProvider<TIdentityResolveProvider>(this IServiceCollection services)
		where TIdentityResolveProvider : class, IIdentityResolveProvider
		=> services
			.AddScoped<IIdentityResolveProvider, TIdentityResolveProvider>();
}
