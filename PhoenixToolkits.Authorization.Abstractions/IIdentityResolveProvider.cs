namespace Valhalla.Authorization;

public interface IIdentityResolveProvider
{
	IAsyncEnumerable<IIdentity> GetIdentitiesAsync(CancellationToken cancellationToken = default);
}
