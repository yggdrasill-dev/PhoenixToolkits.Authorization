namespace Valhalla.Authorization;

public interface IAuthorizationFunction
{
	Guid Id { get; }

	string Name { get; }

	bool AllowAnonymous { get; }
}
