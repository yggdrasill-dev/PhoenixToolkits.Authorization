namespace Valhalla.Authorization.AspNetCore;

public class HttpCompositeFeature : IHttpFeature
{
	private readonly IHttpFeature[] m_Features;

	public HttpCompositeFeature(params IHttpFeature[] features)
	{
		m_Features = features ?? throw new ArgumentNullException(nameof(features));
	}

	public bool IsMatch(HttpContext httpContext)
	{
		foreach (var feature in m_Features)
			if (!feature.IsMatch(httpContext))
				return false;

		return true;
	}
}
