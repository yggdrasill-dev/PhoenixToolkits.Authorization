namespace Valhalla.Authorization;

internal class AuthorizationSystem : IAuthorizationSystem
{
	private readonly IAuthorizationDataStore m_AuthorizationDataStore;

	public string Name { get; }

	public AuthorizationSystem(string systemName, IAuthorizationDataStore authorizationDataStore)
	{
		Name = systemName;
		m_AuthorizationDataStore = authorizationDataStore ?? throw new ArgumentNullException(nameof(authorizationDataStore));
	}

	public ValueTask<bool> HasPermissionAsync(
		IAuthorizationFunction function,
		IAuthorizationIdentityResolveProvider identityResolver,
		CancellationToken cancellationToken = default)
		=> function.AllowAnonymous
			? ValueTask.FromResult(true)
			: m_AuthorizationDataStore.CheckHasPermissionAsync(
			Name,
			function,
			identityResolver.GetIdentitiesAsync(cancellationToken).ToEnumerable(),
			cancellationToken);

	public ValueTask<IAuthorizationFunction?> GetFunctionAsync(string functionName, CancellationToken cancellationToken = default)
		=> m_AuthorizationDataStore.FindFunctionAsync(Name, functionName, cancellationToken);

	public async ValueTask<IAuthorizationFunction?> GetFunctionAsync(IAuthorizationFunctionMatcher functionMatcher, CancellationToken cancellationToken = default)
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
