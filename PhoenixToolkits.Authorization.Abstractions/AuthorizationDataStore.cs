using System.Collections.ObjectModel;

namespace Valhalla.Authorization;

public abstract class AuthorizationDataStore<TFunctionEntity>(
	IEnumerable<IAuthorizationFunctionFactory<TFunctionEntity>> functionFactories)
	: IAuthorizationDataStore
{
	private readonly ReadOnlyDictionary<string, IAuthorizationFunctionFactory<TFunctionEntity>> m_FunctionFactories = functionFactories
		.ToDictionary(fac => fac.Name)
		.AsReadOnly();

	public abstract IAsyncEnumerable<IAuthorizationFunction> GetFunctionsAsync(string system, CancellationToken cancellationToken = default);

	public abstract ValueTask<IAuthorizationFunction?> FindFunctionAsync(string system, string functionName, CancellationToken cancellationToken = default);

	public abstract ValueTask<bool> CheckHasPermissionAsync(string system, IAuthorizationFunction function, IEnumerable<IAuthorizationIdentity> identities, CancellationToken cancellationToken = default);

	protected IAuthorizationFunction? TryCreateFunction(string factoryName, TFunctionEntity entity)
		=> m_FunctionFactories.TryGetValue(factoryName, out var factory)
			? factory.CreateFunction(entity)
			: null;
}
