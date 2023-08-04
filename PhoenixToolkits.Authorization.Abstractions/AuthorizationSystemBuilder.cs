using Microsoft.Extensions.DependencyInjection;

namespace Valhalla.Authorization;

public class AuthorizationSystemBuilder
{
	public IServiceCollection Services { get; }

	internal AuthorizationSystemBuilder(IServiceCollection services)
	{
		Services = services;
	}

	public AuthorizationSystemBuilder RegisterIdentityResolveProvider<TIdentityResolveProvider>()
		where TIdentityResolveProvider : class, IAuthorizationIdentityResolveProvider
	{
		Services.AddScoped<IAuthorizationIdentityResolveProvider, TIdentityResolveProvider>();

		return this;
	}

	public AuthorizationSystemBuilder RegisterAuthorizationDataStore<TAuthorizationDataStore>()
		where TAuthorizationDataStore : class, IAuthorizationDataStore
	{
		Services.AddSingleton<IAuthorizationDataStore, TAuthorizationDataStore>();

		return this;
	}

	public AuthorizationSystemBuilder RegisterFunctionFactory<TFunctionFactory, TFunctionEntity>()
		where TFunctionFactory : class, IAuthorizationFunctionFactory<TFunctionEntity>
	{
		Services.AddSingleton<IAuthorizationFunctionFactory<TFunctionEntity>, TFunctionFactory>();

		return this;
	}
}
