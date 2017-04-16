using Autofac;
using System;
using System.Reflection;

namespace FluffySpoon.Testing.Autofake.Autofac
{
	public class AutofacInversionOfControlRegistration : IInversionOfControlRegistration
	{
		private readonly ContainerBuilder _containerBuilder;

		public AutofacInversionOfControlRegistration(
			ContainerBuilder containerBuilder)
		{
			_containerBuilder = containerBuilder;
		}

		public void RegisterInterfaceTypeAsInstance<TInterface>(
			object instance)
		{
			_containerBuilder.Register(c => (TInterface)instance);
		}
	}
}
