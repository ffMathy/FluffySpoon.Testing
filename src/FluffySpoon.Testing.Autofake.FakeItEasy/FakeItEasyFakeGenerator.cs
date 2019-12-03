using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using FakeItEasy;

namespace FluffySpoon.Testing.Autofake.FakeItEasy
{
	public class FakeItEasyFakeGenerator : IFakeGenerator
	{
		public IReadOnlyList<IFakeInstanceFactory> GenerateFakeInstanceFactories(Type interfaceType)
        {
            if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                interfaceType = interfaceType
                    .GetGenericArguments()
                    .Single();
            }

            var genericEnumerableInterfaceType = typeof(IEnumerable<>).MakeGenericType(interfaceType);
            var genericListInterfaceType = typeof(List<>).MakeGenericType(interfaceType);
            var genericListInterfaceInstance = Activator.CreateInstance(genericListInterfaceType);

            var genericListAddMethod = genericListInterfaceType.GetMethod(nameof(List<object>.Add));
            Debug.Assert(genericListAddMethod != null, nameof(genericListAddMethod) + " != null");

            var fakeInstanceFactoryMethod = typeof(A)
                .GetMethods(
                    BindingFlags.Static | 
                    BindingFlags.Public)
                .Single(x => 
                    x.Name == nameof(A.Fake) &&
                    x.IsGenericMethod &&
                    x.GetParameters().Length == 0);

            var fakeInstanceFactoryMethodGeneric = fakeInstanceFactoryMethod.MakeGenericMethod(interfaceType);

            var fakeInstance = fakeInstanceFactoryMethodGeneric.Invoke(
                null,
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
