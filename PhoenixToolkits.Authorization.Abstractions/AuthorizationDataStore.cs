namespace Valhalla.Authorization;

public abstract class AuthorizationDataStore<TFunctionEntity> : IAuthorizationDataStore
{
	private readonly IReadOnlyDictionary<string, IFunctionFactory<TFunctionEntity>> m_FunctionFactories;

	public AuthorizationDataStore(IEnumerable<IFunctionFactory<TFunctionEntity>> functionFactories)
	{
		m_FunctionFactories = functionFactories.ToDictionary(fac => fac.Name);
	}

	public abstract IAsyncEnumerable<IFunction> GetFunctionsAsync(string system, CancellationToken cancellationToken = default);

	public abstract ValueTask<IFunction?> FindFunctionAsync(string system, string functionName, CancellationToken cancellationToken = default);

	public abstract ValueTask<bool> CheckHasPermissionAsync(string system, IFunction function, IEnumerable<IIdentity> identities, CancellationToken cancellationToken = default);

	protected IFunction? TryCreateFunction(string factoryName, TFunctionEntity entity)
		=> m_FunctionFactories.TryGetValue(factoryName, out var factory)
			? factory.CreateFunction(entity)
			: null;
}
