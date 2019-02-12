using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Moq;

namespace FluffySpoon.Testing.Autofake.Moq
{
	public class MoqFakeGenerator : IFakeGenerator
	{
		public IReadOnlyList<IFakeInstanceFactory> GenerateFakeInstanceFactories(Type interfaceType)
		{
			var genericMockType = typeof(Mock<>).MakeGenericType(interfaceType);
			var genericMockInterfaceType = typeof(IMock<>).MakeGenericType(interfaceType);
			var genericMockInstance = Activator.CreateInstance(genericMockType);
			
			var genericMockInstanceProperty = genericMockType
				.GetProperties(
					BindingFlags.Instance | 
					BindingFlags.Public)
				.Single(p => 
					p.Name == nameof(IMock<object>.Object) &&
					p.PropertyType == interfaceType);
			var genericMockInstanceAccessor = (Func<object>)(() => genericMockInstanceProperty
				.GetMethod
				.Invoke(
					genericMockInstance,
					new object[0]));

			return new IFakeInstanceFactory[] {
				new FakeInstanceFactory(
					interfaceType,
					genericMockInstanceAccessor),
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
