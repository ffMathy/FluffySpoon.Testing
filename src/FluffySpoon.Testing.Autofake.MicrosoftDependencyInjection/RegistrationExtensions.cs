using FluffySpoon.Testing.Autofake.Autofac;
using Microsoft.Extensions.DependencyInjection;

namespace FluffySpoon.Testing.Autofake
{
	public static class RegistrationExtensions
    {
		public static void UseAutofac(
			this Autofaker autofaker,
			IServiceCollection serviceCollection)
		{
			autofaker.Configure(
				new MicrosoftDependencyInjectionInversionOfControlRegistration(serviceCollection));
		}
    }
}
