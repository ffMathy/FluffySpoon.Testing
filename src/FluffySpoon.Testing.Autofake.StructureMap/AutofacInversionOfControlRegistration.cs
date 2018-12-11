using StructureMap;
using System;

namespace FluffySpoon.Testing.Autofake.StructureMap
{
	public class StructureMapInversionOfControlRegistration : IInversionOfControlRegistration
	{
		private readonly Container _container;

		public StructureMapInversionOfControlRegistration(
			Container containerBuilder)
		{
			_container = containerBuilder;
		}

		public void RegisterInterfaceTypeAsInstanceFromAccessor<TInterface>(Func<object> instanceAccessor)
		{
			_container.Configure(x => x.For<TInterface>().Use(() => (TInterface)instanceAccessor()));
		}
	}
}
