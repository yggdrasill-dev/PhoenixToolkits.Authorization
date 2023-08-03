using Valhalla.Authorization.AspNetCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class ApplicationBuilderExtensions
{
	public static IApplicationBuilder UseCaptureFunction(this IApplicationBuilder app)
		=> app.UseMiddleware<MatchFunctionMiddleware>();
}
