namespace Valhalla.Authorization;

public interface IAuthorizationFunctionFactory<TFunctionEntity>
{
	string Name { get; }

	IAuthorizationFunction? CreateFunction(TFunctionEntity entity);
}
