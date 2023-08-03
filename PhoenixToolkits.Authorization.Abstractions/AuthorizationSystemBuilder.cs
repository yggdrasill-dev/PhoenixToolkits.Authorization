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
		where TIdentityResolveProvider : class, IIdentityResolveProvider
	{
		Services.AddScoped<IIdentityResolveProvider, TIdentityResolveProvider>();

		return this;
	}

	public AuthorizationSystemBuilder RegisterAuthorizationDataStore<TAuthorizationDataStore>()
		where TAuthorizationDataStore : class, IAuthorizationDataStore
	{
		Services.AddTransient<IAuthorizationDataStore, TAuthorizationDataStore>();

		return this;
	}

	public AuthorizationSystemBuilder RegisterFunctionFactory<TFunctionFactory, TFunctionEntity>()
		where TFunctionFactory : class, IFunctionFactory<TFunctionEntity>
	{
		Services.AddSingleton<IFunctionFactory<TFunctionEntity>, TFunctionFactory>();

		return this;
	}
}
