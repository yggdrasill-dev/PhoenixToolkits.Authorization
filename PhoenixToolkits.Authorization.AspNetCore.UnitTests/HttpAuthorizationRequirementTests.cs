using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Valhalla.Authorization;
using Valhalla.Authorization.AspNetCore;

namespace PhoenixToolkits.Authorization.AspNetCore.UnitTests;

public class HttpAuthorizationRequirementTests
{
    [Fact]
    public async Task 使用權限驗證物件判斷是否有權限()
    {
        // Arrange
        var sut = new HttpAuthorizationRequirement();

        var user = new ClaimsPrincipal();
        var function = Substitute.For<IAuthorizationFunction>();
        var system = Substitute.For<IAuthorizationSystem>();
        var idResolver = Substitute.For<IAuthorizationIdentityResolveProvider>();

        var httpContext = new DefaultHttpContext();
        httpContext.Features.Set(function);
        httpContext.RequestServices = new ServiceCollection()
            .AddSingleton(system)
            .AddSingleton(idResolver)
            .BuildServiceProvider();

        var context = new AuthorizationHandlerContext(new[] { sut }, user, httpContext);

        _ = system.HasPermissionAsync(Arg.Is(function), Arg.Is(idResolver))
            .Returns(true);

        // Act
        await sut.HandleAsync(context);

        // Assert
        Assert.True(context.HasSucceeded);
    }

    [Fact]
    public async Task 找不到Function的話就會判斷成沒有權限()
    {
        // Arrange
        var sut = new HttpAuthorizationRequirement();

        var user = new ClaimsPrincipal();
        var system = Substitute.For<IAuthorizationSystem>();
        var idResolver = Substitute.For<IAuthorizationIdentityResolveProvider>();

        var httpContext = new DefaultHttpContext
        {
            RequestServices = new ServiceCollection()
                .AddSingleton(system)
                .AddSingleton(idResolver)
                .BuildServiceProvider()
        };

        var context = new AuthorizationHandlerContext(new[] { sut }, user, httpContext);

        // Act
        await sut.HandleAsync(context);

        // Assert
        Assert.False(context.HasSucceeded);
        Assert.True(context.HasFailed);
    }

    [Fact]
    public async Task 如果System物件的HasPermission判斷為False則沒有權限()
    {
        // Arrange
        var sut = new HttpAuthorizationRequirement();

        var user = new ClaimsPrincipal();
        var function = Substitute.For<IAuthorizationFunction>();
        var system = Substitute.For<IAuthorizationSystem>();
        var idResolver = Substitute.For<IAuthorizationIdentityResolveProvider>();

        var httpContext = new DefaultHttpContext();
        httpContext.Features.Set(function);
        httpContext.RequestServices = new ServiceCollection()
            .AddSingleton(system)
            .AddSingleton(idResolver)
            .BuildServiceProvider();

        var context = new AuthorizationHandlerContext(new[] { sut }, user, httpContext);

        _ = system.HasPermissionAsync(Arg.Is(function), Arg.Is(idResolver))
            .Returns(false);

        // Act
        await sut.HandleAsync(context);

        // Assert
        Assert.False(context.HasSucceeded);
        Assert.True(context.HasFailed);
    }
}
