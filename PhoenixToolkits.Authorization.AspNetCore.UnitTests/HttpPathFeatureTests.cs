using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Valhalla.Authorization.AspNetCore;

namespace PhoenixToolkits.Authorization.AspNetCore.UnitTests;

public class HttpPathFeatureTests
{
    [Fact]
    public void HttpPathFeature_以AspNetCore的Route字串來判斷要求是否符合()
    {
        // Arrange
        var sut = new HttpPathFeature("api/test/{id:int}", new ServiceCollection().BuildServiceProvider());

        var httpContext = new DefaultHttpContext();
        httpContext.Request.Path = "/api/test/123";

        // Act
        var actual = sut.IsMatch(httpContext);

        // Assert
        Assert.True(actual);
    }
}
