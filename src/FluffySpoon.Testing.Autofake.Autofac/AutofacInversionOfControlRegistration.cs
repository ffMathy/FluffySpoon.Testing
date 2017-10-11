using Autofac;
using System;

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

		public void RegisterInterfaceTypeAsInstanceFromAccessor<TInterface>(Func<object> instanceAccessor)
		{
			_containerBuilder.Register(c => (TInterface)instanceAccessor());
		}
	}
}
