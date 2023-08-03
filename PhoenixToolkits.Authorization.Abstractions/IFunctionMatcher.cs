namespace Valhalla.Authorization;

public interface IFunctionMatcher
{
	bool IsMatch(IFunction function);
}
