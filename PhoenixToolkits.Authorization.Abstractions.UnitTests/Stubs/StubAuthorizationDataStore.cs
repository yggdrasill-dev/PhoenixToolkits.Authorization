using Valhalla.Authorization;

namespace PhoenixToolkits.Authorization.Abstractions.UnitTests.Stubs;

internal class StubAuthorizationDataStore : IAuthorizationDataStore
{
    public IAsyncEnumerable<IAuthorizationFunction> GetFunctionsAsync(string system, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public ValueTask<IAuthorizationFunction?> FindFunctionAsync(string system, string functionName, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public ValueTask<bool> CheckHasPermissionAsync(string system, IAuthorizationFunction function, IEnumerable<IAuthorizationIdentity> identities, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
