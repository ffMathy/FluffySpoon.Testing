using System;
using System.Collections.Generic;

namespace FluffySpoon.Testing.Autofake
{
	public interface IFakeGenerator
	{
		IReadOnlyList<IFakeInstanceFactory> GenerateFakeInstanceFactories(Type interfaceType);
	}
}