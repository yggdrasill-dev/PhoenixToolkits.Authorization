namespace Valhalla.Authorization;

internal class AuthorizationSystem(
	string systemName,
	IAuthorizationDataStore authorizationDataStore)
	: IAuthorizationSystem
{
	public string Name { get; } = systemName;

	public ValueTask<bool> HasPermissionAsync(
		IAuthorizationFunction function,
		IAuthorizationIdentityResolveProvider identityResolver,
		CancellationToken cancellationToken = default)
		=> function.AllowAnonymous
			? ValueTask.FromResult(true)
			: authorizationDataStore.CheckHasPermissionAsync(
			Name,
			function,
			identityResolver.GetIdentitiesAsync(cancellationToken).ToEnumerable(),
			cancellationToken);

	public ValueTask<IAuthorizationFunction?> GetFunctionAsync(string functionName, CancellationToken cancellationToken = default)
		=> authorizationDataStore.FindFunctionAsync(Name, functionName, cancellationToken);

	public async ValueTask<IAuthorizationFunction?> GetFunctionAsync(IAuthorizationFunctionMatcher functionMatcher, CancellationToken cancellationToken = default)
	{
		await foreach (var function in authorizationDataStore.GetFunctionsAsync(Name, cancellationToken)
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
