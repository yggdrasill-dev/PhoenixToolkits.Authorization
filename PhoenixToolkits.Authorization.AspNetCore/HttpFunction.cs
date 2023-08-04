namespace Valhalla.Authorization.AspNetCore;

public sealed class HttpFunction : IAuthorizationFunction
{
	private readonly IReadOnlyCollection<IHttpFeature> m_Features;

	public bool AllowAnonymous { get; }

	public Guid Id { get; }

	public string Name { get; }

	public HttpFunction(Guid id, string name, bool allowAnonymous, params IHttpFeature[] features)
	{
		Id = id;
		Name = name;
		AllowAnonymous = allowAnonymous;
		m_Features = Array.AsReadOnly(features);
	}

	public bool IsMatch(HttpContext httpContext)
	{
		foreach (var feature in m_Features)
			if (feature.IsMatch(httpContext))
				return true;

		return false;
	}
}
