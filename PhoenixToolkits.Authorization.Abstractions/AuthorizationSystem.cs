namespace Valhalla.Authorization;

internal class AuthorizationSystem : ISystem
{
	private readonly IAuthorizationDataStore m_AuthorizationDataStore;

	public string Name { get; }

	public AuthorizationSystem(string systemName, IAuthorizationDataStore authorizationDataStore)
	{
		Name = systemName;
		m_AuthorizationDataStore = authorizationDataStore ?? throw new ArgumentNullException(nameof(authorizationDataStore));
	}

	public ValueTask<bool> HasPermissionAsync(
		IFunction function,
		IIdentityResolveProvider identityResolver,
		CancellationToken cancellationToken = default)
		=> function.AllowAnonymous
			? ValueTask.FromResult(true)
			: m_AuthorizationDataStore.CheckHasPermissionAsync(
			Name,
			function,
			identityResolver.GetIdentitiesAsync(cancellationToken).ToEnumerable(),
			cancellationToken);

	public ValueTask<IFunction?> GetFunctionAsync(string functionName, CancellationToken cancellationToken = default)
		=> m_AuthorizationDataStore.FindFunctionAsync(Name, functionName, cancellationToken);

	public async ValueTask<IFunction?> GetFunctionAsync(IFunctionMatcher functionMatcher, CancellationToken cancellationToken = default)
	{
		await foreach (var function in m_AuthorizationDataStore.GetFunctionsAsync(Name, cancellationToken)
			.WithCancellation(cancellationToken)
			.ConfigureAwait(false))
		{
			if (functionMatcher.IsMatch(function))
			{
				return function;
			}
		}

		return null;
	}
}
