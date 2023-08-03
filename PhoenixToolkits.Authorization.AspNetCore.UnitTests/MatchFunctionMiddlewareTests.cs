using Microsoft.AspNetCore.Http;
using NSubstitute;
using Valhalla.Authorization;
using Valhalla.Authorization.AspNetCore;

namespace PhoenixToolkits.Authorization.AspNetCore.UnitTests;

public class MatchFunctionMiddlewareTests
{
    [Fact]
    public async Task 使用ISystem來判斷要求是否有符合的Function()
    {
        // Arrange
        var fakeSystem = Substitute.For<ISystem>();
        var sut = new MatchFunctionMiddleware(fakeSystem);

        var httpContext = new DefaultHttpContext();

        var function = Substitute.For<IFunction>();
        _ = fakeSystem.GetFunctionAsync(Arg.Any<IFunctionMatcher>())
            .Returns(function);

        // Act
        await sut.InvokeAsync(httpContext, ctx => Task.CompletedTask);

        // Assert
        Assert.Equal(function, httpContext.Features.Get<IFunction>());
    }
}
