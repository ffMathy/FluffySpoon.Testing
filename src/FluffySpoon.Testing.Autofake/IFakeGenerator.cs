using System;
using System.Reflection;

namespace FluffySpoon.Testing.Autofake
{
	public interface IFakeGenerator
	{
		object GenerateFake(Type interfaceType);
	}
}