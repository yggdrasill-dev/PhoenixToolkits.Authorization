using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Valhalla.Authorization;
using Xunit;

namespace PhoenixToolkits.Authorization.Abstractions.UnitTests;
public class DependencyInjectionTests
{
    [Fact]
    public void DI註冊測試()
    {
        // Arrange
        var services = new ServiceCollection()
            .AddPhoenixAuthorizationKits()
            .Services;

        var sut = services.BuildServiceProvider();

        // Act
        sut.GetRequiredService<IAuthorizationSystem>();

        // Assert

    }
}
