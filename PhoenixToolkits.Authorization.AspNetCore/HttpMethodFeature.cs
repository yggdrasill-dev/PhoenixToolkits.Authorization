using Microsoft.AspNetCore.Routing.Constraints;

namespace Valhalla.Authorization.AspNetCore;

public class HttpMethodFeature(params string[] httpMethods) : IHttpFeature
{
	private readonly HttpMethodRouteConstraint m_Constraint = new(httpMethods);

	public bool IsMatch(HttpContext httpContext)
		=> m_Constraint.Match(
			httpContext,
			null,
			string.Empty,
			[],
			RouteDirection.IncomingRequest);
}
