using System;
using System.Reflection;

namespace FluffySpoon.Autofake
{
	public interface IFakeGenerator
	{
		object GenerateFake(Type interfaceType);
	}
}