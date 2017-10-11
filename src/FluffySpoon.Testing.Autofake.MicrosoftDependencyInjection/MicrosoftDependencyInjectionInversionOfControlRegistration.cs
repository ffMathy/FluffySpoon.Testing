using Microsoft.Extensions.DependencyInjection;
using System;

namespace FluffySpoon.Testing.Autofake.Autofac
{
	public class MicrosoftDependencyInjectionInversionOfControlRegistration : IInversionOfControlRegistration
	{
		private readonly IServiceCollection _serviceCollection;

		public MicrosoftDependencyInjectionInversionOfControlRegistration(
			IServiceCollection serviceCollection)
		{
			_serviceCollection = serviceCollection;
		}

		public void RegisterInterfaceTypeAsInstanceFromAccessor<TInterface>(
			Func<object> instanceAccessor)
		{
			_serviceCollection.AddSingleton(typeof(TInterface), p => instanceAccessor());
		}
	}
}
