﻿using NSubstitute;
using Valhalla.Authorization;

namespace PhoenixToolkits.Authorization.Abstractions.UnitTests;

public class AuthorizationSystemTests
{
    [Fact]
    public async Task 判斷功能能否存取_依據使用者的識別物件_呼叫IAuthorizationDataStore來判斷結果()
    {
        // Arrange
        var fakeAuthorizationDataStore = Substitute.For<IAuthorizationDataStore>();

        var sut = new AuthorizationSystem("Test", fakeAuthorizationDataStore);

        var function = Substitute.For<IFunction>();
        var idResolver = Substitute.For<IIdentityResolveProvider>();

        // Act
        _ = await sut.HasPermissionAsync(function, idResolver);

        // Assert
        _ = fakeAuthorizationDataStore.Received(1)
            .CheckHasPermissionAsync(
                Arg.Is(sut.Name),
                Arg.Is(function),
                Arg.Any<IEnumerable<IIdentity>>());
    }

    [Fact]
    public async Task 判斷功能能否存取_如果功能設定允許匿名_會直接回傳能存取()
    {
        // Arrange
        var fakeAuthorizationDataStore = Substitute.For<IAuthorizationDataStore>();

        var sut = new AuthorizationSystem("Test", fakeAuthorizationDataStore);

        var function = Substitute.For<IFunction>();
        var idResolver = Substitute.For<IIdentityResolveProvider>();

        _ = function.AllowAnonymous.Returns(true);

        // Act
        var actual = await sut.HasPermissionAsync(function, idResolver);

        // Assert
        Assert.True(actual);

        _ = fakeAuthorizationDataStore.Received(0)
            .CheckHasPermissionAsync(
                Arg.Is(sut.Name),
                Arg.Is(function),
                Arg.Any<IEnumerable<IIdentity>>());
    }

    [Fact]
    public async Task 依據FunctionName取得功能()
    {
        // Arrange
        var fakeAuthorizationDataStore = Substitute.For<IAuthorizationDataStore>();

        var sut = new AuthorizationSystem("Test", fakeAuthorizationDataStore);

        // Act
        _ = await sut.GetFunctionAsync("FunctionName");

        // Assert
        _ = fakeAuthorizationDataStore.Received(1)
            .FindFunctionAsync(Arg.Is(sut.Name), Arg.Is("FunctionName"));
    }

    [Fact]
    public async Task 使用IFunctionMatcher來判定要求是否符合某功能()
    {
        // Arrange
        var fakeAuthorizationDataStore = Substitute.For<IAuthorizationDataStore>();

        var sut = new AuthorizationSystem("Test", fakeAuthorizationDataStore);

        var functionMatcher = Substitute.For<IFunctionMatcher>();

        var systemFunctions = new[]
        {
            Substitute.For<IFunction>(),
            Substitute.For<IFunction>(),
            Substitute.For<IFunction>()
        };

        _ = fakeAuthorizationDataStore.GetFunctionsAsync(Arg.Is(sut.Name))
            .Returns(systemFunctions.ToAsyncEnumerable());
        _ = functionMatcher.IsMatch(Arg.Any<IFunction>())
            .Returns(callInfo =>
            {
                var function = callInfo.Arg<IFunction>();

                return function == systemFunctions[1];
            });

        // Act
        var actual = await sut.GetFunctionAsync(functionMatcher);

        // Assert
        _ = fakeAuthorizationDataStore.Received(1)
            .GetFunctionsAsync(Arg.Is(sut.Name));
        _ = functionMatcher.Received(2)
            .IsMatch(Arg.Any<IFunction>());
        Assert.Equal(systemFunctions[1], actual);
    }
}
