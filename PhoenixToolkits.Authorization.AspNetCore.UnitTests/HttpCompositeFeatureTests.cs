using Microsoft.AspNetCore.Http;
using NSubstitute;
using Valhalla.Authorization.AspNetCore;
using Xunit;

namespace PhoenixToolkits.Authorization.AspNetCore.UnitTests;

public class HttpCompositeFeatureTests
{
    [Fact]
    public void HttpCompositeFeature_判斷要求是否符合需要底下所有的Feature都Match才符合()
    {
        // Arrange
        var fakeFeature1 = Substitute.For<IHttpFeature>();
        _ = fakeFeature1.IsMatch(Arg.Any<HttpContext>())
            .Returns(true);
        var fakeFeature2 = Substitute.For<IHttpFeature>();
        _ = fakeFeature2.IsMatch(Arg.Any<HttpContext>())
            .Returns(true);

        var sut = new HttpCompositeFeature(fakeFeature1, fakeFeature2);

        var context = new DefaultHttpContext();

        // Act
        var actual = sut.IsMatch(context);

        // Assert
        Assert.True(actual);
    }

    [Fact]
    public void HttpCompositeFeature_如果建立時沒有任何子Feature則任何要求都Match()
    {
        // Arrange
        var sut = new HttpCompositeFeature();

        var context = new DefaultHttpContext();

        // Act
        var actual = sut.IsMatch(context);

        // Assert
        Assert.True(actual);
    }

    [Fact]
    public void HttpCompositeFeature_任何一個子Feature不Match就要求不符合()
    {
        // Arrange
        var fakeFeature1 = Substitute.For<IHttpFeature>();
        _ = fakeFeature1.IsMatch(Arg.Any<HttpContext>())
            .Returns(true);
        var fakeFeature2 = Substitute.For<IHttpFeature>();
        _ = fakeFeature2.IsMatch(Arg.Any<HttpContext>())
            .Returns(false);

        var sut = new HttpCompositeFeature(fakeFeature1, fakeFeature2);

        var context = new DefaultHttpContext();

        // Act
        var actual = sut.IsMatch(context);

        // Assert
        Assert.False(actual);
    }
}
