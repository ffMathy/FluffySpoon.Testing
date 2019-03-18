using System;
using Lamar;

namespace FluffySpoon.Testing.Autofake.Lamar
{
	public class LamarInversionOfControlRegistration : IInversionOfControlRegistration
	{
        private readonly ServiceRegistry _serviceRegistry;

        public LamarInversionOfControlRegistration(
			ServiceRegistry serviceRegistry)
        {
            _serviceRegistry = serviceRegistry;
        }

		public void RegisterInterfaceTypeAsInstanceFromAccessor<TInterface>(Func<object> instanceAccessor) where TInterface : class
        {
			_serviceRegistry.For<TInterface>().Use(c => (TInterface)instanceAccessor());
		}
	}
}
