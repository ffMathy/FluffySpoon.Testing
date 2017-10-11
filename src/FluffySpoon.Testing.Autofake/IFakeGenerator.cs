using System;

namespace FluffySpoon.Testing.Autofake
{
	public interface IFakeGenerator
	{
		object GenerateFake(Type interfaceType);
	}
}