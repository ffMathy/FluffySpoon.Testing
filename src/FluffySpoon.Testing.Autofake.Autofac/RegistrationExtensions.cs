using Autofac;
using FluffySpoon.Testing.Autofake.Autofac;

namespace FluffySpoon.Testing.Autofake
{
	public static class RegistrationExtensions
    {
		public static void UseAutofac(
			this Autofaker autofaker,
			ContainerBuilder containerBuilder)
		{
			autofaker.Configure(
				new AutofacInversionOfControlRegistration(containerBuilder));
		}
    }
}
