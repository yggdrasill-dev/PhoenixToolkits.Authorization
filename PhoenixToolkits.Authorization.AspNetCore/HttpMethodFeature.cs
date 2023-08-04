using Microsoft.AspNetCore.Routing.Constraints;

namespace Valhalla.Authorization.AspNetCore;

public class HttpMethodFeature : IHttpFeature
{
	private readonly HttpMethodRouteConstraint m_Constraint;

	public HttpMethodFeature(params string[] httpMethods)
	{
		m_Constraint = new HttpMethodRouteConstraint(httpMethods);
	}

	public bool IsMatch(HttpContext httpContext)
		=> m_Constraint.Match(
			httpContext,
			null,
			string.Empty,
			new RouteValueDictionary(),
			RouteDirection.IncomingRequest);
}
