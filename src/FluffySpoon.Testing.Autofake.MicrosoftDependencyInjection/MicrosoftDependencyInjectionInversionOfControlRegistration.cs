using Microsoft.Extensions.DependencyInjection;

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

		public void RegisterInterfaceTypeAsInstance<TInterface>(
			object instance)
		{
			_serviceCollection.AddSingleton(typeof(TInterface), instance);
		}
	}
}
