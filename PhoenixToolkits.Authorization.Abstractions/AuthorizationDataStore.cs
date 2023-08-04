namespace Valhalla.Authorization;

public abstract class AuthorizationDataStore<TFunctionEntity> : IAuthorizationDataStore
{
	private readonly IReadOnlyDictionary<string, IAuthorizationFunctionFactory<TFunctionEntity>> m_FunctionFactories;

	public AuthorizationDataStore(IEnumerable<IAuthorizationFunctionFactory<TFunctionEntity>> functionFactories)
	{
		m_FunctionFactories = functionFactories.ToDictionary(fac => fac.Name);
	}

	public abstract IAsyncEnumerable<IAuthorizationFunction> GetFunctionsAsync(string system, CancellationToken cancellationToken = default);

	public abstract ValueTask<IAuthorizationFunction?> FindFunctionAsync(string system, string functionName, CancellationToken cancellationToken = default);

	public abstract ValueTask<bool> CheckHasPermissionAsync(string system, IAuthorizationFunction function, IEnumerable<IAuthorizationIdentity> identities, CancellationToken cancellationToken = default);

	protected IAuthorizationFunction? TryCreateFunction(string factoryName, TFunctionEntity entity)
		=> m_FunctionFactories.TryGetValue(factoryName, out var factory)
			? factory.CreateFunction(entity)
			: null;
}
