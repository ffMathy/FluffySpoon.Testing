using NSubstitute;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FluffySpoon.Testing.Autofake.NSubstitute
{
	public class NSubstituteFakeGenerator : IFakeGenerator
	{
		public IReadOnlyList<IFakeInstanceFactory> GenerateFakeInstanceFactories(Type interfaceType)
        {
            var genericEnumerableInterfaceType = typeof(IEnumerable<>).MakeGenericType(interfaceType);
            var genericListInterfaceType = typeof(List<>).MakeGenericType(interfaceType);
            var genericListInterfaceInstance = Activator.CreateInstance(genericListInterfaceType);

            var genericListAddMethod = genericListInterfaceType.GetMethod(nameof(List<object>.Add));
            Debug.Assert(genericListAddMethod != null, nameof(genericListAddMethod) + " != null");

            var fakeInstance = Substitute.For(
                new Type[] { interfaceType },
                new object[0]);

            genericListAddMethod.Invoke(
                genericListInterfaceInstance,
                new[]
                {
                    fakeInstance
                });

            return new IFakeInstanceFactory[] {
				new FakeInstanceFactory(
					interfaceType,
					() => fakeInstance),
                new FakeInstanceFactory(
                    genericEnumerableInterfaceType,
                    () => genericListInterfaceInstance)
            };
		}
	}
}
