namespace Valhalla.Authorization;

public interface IAuthorizationFunctionMatcher
{
	bool IsMatch(IAuthorizationFunction function);
}
