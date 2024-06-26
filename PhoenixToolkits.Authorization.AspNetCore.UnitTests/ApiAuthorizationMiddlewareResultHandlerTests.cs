﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using Valhalla.Authorization.AspNetCore;

namespace PhoenixToolkits.Authorization.AspNetCore.UnitTests;

public class ApiAuthorizationMiddlewareResultHandlerTests
{
    [Fact]
    public async Task ApiAuthorizationMiddlewareResultHandler_Forbid時_將StatusCode設定為403()
    {
        // Arrange
        var sut = new ApiAuthorizationMiddlewareResultHandler();

        var context = new DefaultHttpContext();
        var policy = new AuthorizationPolicy(
            [Substitute.For<IAuthorizationRequirement>()],
            []);
        var authorizeResult = PolicyAuthorizationResult.Forbid();

        // Act
        await sut.HandleAsync(
            (ctx) => Task.CompletedTask,
            context,
            policy,
            authorizeResult);

        // Assert
        Assert.Equal(403, context.Response.StatusCode);
    }
}
