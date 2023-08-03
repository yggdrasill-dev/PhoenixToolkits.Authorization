using Microsoft.AspNetCore.Http;
using NSubstitute;
using Valhalla.Authorization.AspNetCore;

namespace PhoenixToolkits.Authorization.AspNetCore.UnitTests;

public class HttpFunctionTests
{
    [Fact]
    public void 以HttpContext的內容來判斷要求是不是符合功能()
    {
        // Arrange
        var fakeHttpFeature = Substitute.For<IHttpFeature>();

        var sut = new HttpFunction(Guid.NewGuid(), "Test", false, fakeHttpFeature);

        var context = new DefaultHttpContext();

        _ = fakeHttpFeature.IsMatch(Arg.Is(context))
            .Returns(true);

        // Act
        var actual = sut.IsMatch(context);

        // Assert
        Assert.True(actual);
    }

    [Fact]
    public void 如果沒有任何HttpFeature判斷永遠不符合()
    {
        // Arrange
        var sut = new HttpFunction(Guid.NewGuid(), "Test", false);

        var context = new DefaultHttpContext();

        // Act
        var actual = sut.IsMatch(context);

        // Assert
        Assert.False(actual);
    }

    [Fact]
    public void 只要任何一個HttpFeature符合就會判斷符合()
    {
        // Arrange
        var feature1 = Substitute.For<IHttpFeature>();
        _ = feature1.IsMatch(Arg.Any<HttpContext>())
            .Returns(false);
        var feature2 = Substitute.For<IHttpFeature>();
        _ = feature2.IsMatch(Arg.Any<HttpContext>())
            .Returns(true);

        var sut = new HttpFunction(
            Guid.NewGuid(),
            "Test",
            false,
            feature1,
            feature2);

        var context = new DefaultHttpContext();

        // Act
        var actual = sut.IsMatch(context);

        // Assert
        Assert.True(actual);
    }
}
