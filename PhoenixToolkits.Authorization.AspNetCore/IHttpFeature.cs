namespace Valhalla.Authorization.AspNetCore;

public interface IHttpFeature
{
	bool IsMatch(HttpContext httpContext);
}
