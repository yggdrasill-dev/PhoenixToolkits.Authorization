using Microsoft.AspNetCore.Http;
using Valhalla.Authorization.AspNetCore;

namespace PhoenixToolkits.Authorization.AspNetCore.UnitTests;

public class HttpMethodFeatureTests
{
    [Fact]
    public void Http要求的Method符合設定IsMatch回傳為True()
    {
        // Arrange
        var sut = new HttpMethodFeature("Get");

        var httpContext = new DefaultHttpContext();
        httpContext.Request.Method = "GET";

        // Act
        var actual = sut.IsMatch(httpContext);

        // Assert
        Assert.True(actual);
    }

    [Fact]
    public void Http要求的Method不符合設定IsMatch回傳為False()
    {
        // Arrange
        var sut = new HttpMethodFeature("Put", "Post", "Delete");

        var httpContext = new DefaultHttpContext();
        httpContext.Request.Method = "GET";

        // Act
        var actual = sut.IsMatch(httpContext);

        // Assert
        Assert.False(actual);
    }
}
