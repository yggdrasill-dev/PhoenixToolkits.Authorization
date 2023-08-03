namespace Valhalla.Authorization;

public interface IFunction
{
	Guid Id { get; }

	string Name { get; }

	bool AllowAnonymous { get; }
}
