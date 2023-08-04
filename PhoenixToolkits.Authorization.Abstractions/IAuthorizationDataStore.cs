namespace Valhalla.Authorization;

public interface IAuthorizationDataStore
{
	IAsyncEnumerable<IAuthorizationFunction> GetFunctionsAsync(string system, CancellationToken cancellationToken = default);

	ValueTask<IAuthorizationFunction?> FindFunctionAsync(string system, string functionName, CancellationToken cancellationToken = default);

	ValueTask<bool> CheckHasPermissionAsync(
		string system,
		IAuthorizationFunction function,
		IEnumerable<IAuthorizationIdentity> identities,
		CancellationToken cancellationToken = default);
}
