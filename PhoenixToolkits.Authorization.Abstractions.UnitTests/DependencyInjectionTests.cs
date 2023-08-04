using Microsoft.Extensions.DependencyInjection;
using PhoenixToolkits.Authorization.Abstractions.UnitTests.Stubs;
using Valhalla.Authorization;

namespace PhoenixToolkits.Authorization.Abstractions.UnitTests;

public class DependencyInjectionTests
{
    [Fact]
    public void DI註冊測試()
    {
        // Arrange
        var services = new ServiceCollection()
            .AddPhoenixAuthorizationKits("Test")
            .RegisterAuthorizationDataStore<StubAuthorizationDataStore>()
            .Services;

        var sut = services.BuildServiceProvider(true);

        // Act
        var actual = sut.GetRequiredService<IAuthorizationSystem>();

        // Assert
        Assert.NotNull(actual);
    }
}
