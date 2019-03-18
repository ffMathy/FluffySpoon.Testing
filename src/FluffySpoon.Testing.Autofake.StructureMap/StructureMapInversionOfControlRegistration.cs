using StructureMap;
using System;

namespace FluffySpoon.Testing.Autofake.StructureMap
{
	public class StructureMapInversionOfControlRegistration : IInversionOfControlRegistration
	{
		private readonly IContainer _container;

		public StructureMapInversionOfControlRegistration(
			IContainer containerBuilder)
		{
			_container = containerBuilder;
		}

		public void RegisterInterfaceTypeAsInstanceFromAccessor<TInterface>(Func<object> instanceAccessor) where TInterface : class
        {
			_container.Configure(x => x.For<TInterface>().Use(() => (TInterface)instanceAccessor()));
		}
	}
}
