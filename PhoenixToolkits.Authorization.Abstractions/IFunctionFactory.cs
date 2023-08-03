namespace Valhalla.Authorization;

public interface IFunctionFactory<TFunctionEntity>
{
	string Name { get; }

	IFunction? CreateFunction(TFunctionEntity entity);
}
