using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Moq;

namespace FluffySpoon.Testing.Autofake.Moq
{
	public class MoqFakeGenerator : IFakeGenerator
	{
		public IReadOnlyList<IFakeInstanceFactory> GenerateFakeInstanceFactories(Type interfaceType)
        {
            if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                interfaceType = interfaceType
                    .GetGenericArguments()
                    .Single();
            }

            var genericMockType = typeof(Mock<>).MakeGenericType(interfaceType);
			var genericMockInterfaceType = typeof(IMock<>).MakeGenericType(interfaceType);
			var genericMockInstance = Activator.CreateInstance(genericMockType);
            var genericEnumerableInterfaceType = typeof(IEnumerable<>).MakeGenericType(interfaceType);
            var genericListInterfaceType = typeof(List<>).MakeGenericType(interfaceType);
            var genericListInterfaceInstance = Activator.CreateInstance(genericListInterfaceType);

            var genericListAddMethod = genericListInterfaceType.GetMethod(nameof(List<object>.Add));
            Debug.Assert(genericListAddMethod != null, nameof(genericListAddMethod) + " != null");

            var genericMockInstanceProperty = genericMockType
                .GetProperties(
                    BindingFlags.Instance |
                    BindingFlags.Public)
                .Single(p =>
                    p.Name == nameof(IMock<object>.Object) &&
                    p.PropertyType == interfaceType);

            genericListAddMethod.Invoke(
                genericListInterfaceInstance,
                new[]
                {
                    InvokeGetPropertyOnTarget(
                        genericMockInstanceProperty,
                        genericMockInstance)
                });

            object InvokeGetPropertyOnTarget(PropertyInfo propertyInfo, object o)
            {
                return propertyInfo
                    .GetMethod
                    .Invoke(
                        o,
                        new object[0]);
            }

            object GenericMockInstanceAccessor()
            {
                return InvokeGetPropertyOnTarget(
                    genericMockInstanceProperty, 
                    genericMockInstance);
            }

            return new IFakeInstanceFactory[] {
                new FakeInstanceFactory(
                    interfaceType,
                    GenericMockInstanceAccessor),
                new FakeInstanceFactory(
                    genericEnumerableInterfaceType,
                    () => genericListInterfaceInstance),
				new FakeInstanceFactory(
					genericMockType,
					() => genericMockInstance),
				new FakeInstanceFactory(
					genericMockInterfaceType,
					() => genericMockInstance)
			};
		}
	}
}
