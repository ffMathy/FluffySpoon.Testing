using NSubstitute;
using System;

namespace FluffySpoon.Testing.Autofake.NSubstitute
{
	public class NSubstituteFakeGenerator : IFakeGenerator
	{
		public object GenerateFake(Type interfaceType)
		{
			return Substitute.For(
				new Type[] { interfaceType }, 
				new object[0]);
		}
	}
}
