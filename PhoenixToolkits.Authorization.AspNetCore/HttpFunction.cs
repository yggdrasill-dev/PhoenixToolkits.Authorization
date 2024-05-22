namespace Valhalla.Authorization.AspNetCore;

public sealed class HttpFunction(
	Guid id,
	string name,
	bool allowAnonymous,
	params IHttpFeature[] features)
	: IAuthorizationFunction
{
	private readonly IReadOnlyCollection<IHttpFeature> m_Features = Array.AsReadOnly(features);

	public bool AllowAnonymous { get; } = allowAnonymous;

	public Guid Id { get; } = id;

	public string Name { get; } = name;

	public bool IsMatch(HttpContext httpContext)
	{
		foreach (var feature in m_Features)
			if (feature.IsMatch(httpContext))
				return true;

		return false;
	}
}
