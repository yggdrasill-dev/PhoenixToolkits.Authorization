namespace Valhalla.Authorization;

public interface ISystem
{
	string Name { get; }

	ValueTask<bool> HasPermissionAsync(
		IFunction function,
		IIdentityResolveProvider identityResolver,
		CancellationToken cancellationToken = default);

	ValueTask<IFunction?> GetFunctionAsync(string functionName, CancellationToken cancellationToken = default);

	ValueTask<IFunction?> GetFunctionAsync(IFunctionMatcher functionMatcher, CancellationToken cancellationToken = default);
}
