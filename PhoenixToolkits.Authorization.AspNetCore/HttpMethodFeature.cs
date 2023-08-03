namespace Valhalla.Authorization.AspNetCore;

public class HttpMethodFeature : IHttpFeature
{
	private readonly HashSet<string> m_HttpMethods;

	public HttpMethodFeature(params string[] httpMethods)
	{
		m_HttpMethods = new HashSet<string>(httpMethods, StringComparer.OrdinalIgnoreCase);
	}

	public bool IsMatch(HttpContext httpContext)
		=> m_HttpMethods.Contains(httpContext.Request.Method);
}
