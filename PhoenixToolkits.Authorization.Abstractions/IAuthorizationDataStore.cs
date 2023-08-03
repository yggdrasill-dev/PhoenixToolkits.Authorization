namespace Valhalla.Authorization;

public interface IAuthorizationDataStore
{
	IAsyncEnumerable<IFunction> GetFunctionsAsync(string system, CancellationToken cancellationToken = default);

	ValueTask<IFunction?> FindFunctionAsync(string system, string functionName, CancellationToken cancellationToken = default);

	ValueTask<bool> CheckHasPermissionAsync(
		string system,
		IFunction function,
		IEnumerable<IIdentity> identities,
		CancellationToken cancellationToken = default);
}
