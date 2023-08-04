namespace Valhalla.Authorization;

public interface IAuthorizationIdentityResolveProvider
{
	IAsyncEnumerable<IAuthorizationIdentity> GetIdentitiesAsync(CancellationToken cancellationToken = default);
}
