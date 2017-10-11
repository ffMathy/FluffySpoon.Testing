using NSubstitute;
using System;
using System.Collections.Generic;

namespace FluffySpoon.Testing.Autofake.NSubstitute
{
	public class NSubstituteFakeGenerator : IFakeGenerator
	{
		public IReadOnlyList<IFakeInstanceFactory> GenerateFakeInstanceFactories(Type interfaceType)
		{
			var fakeInstance = Substitute.For(
				new Type[] { interfaceType },
				new object[0]);
			return new IFakeInstanceFactory[] {
				new FakeInstanceFactory(
					interfaceType,
					() => fakeInstance)
			};
		}
	}
}
