using FluffySpoon.Testing.Autofake.Lamar;
using Lamar;

namespace FluffySpoon.Testing.Autofake
{
	public static class RegistrationExtensions
    {
		public static void UseLamar(
			this Autofaker autofaker,
			ServiceRegistry serviceRegistry)
		{
			autofaker.Configure(
				new LamarInversionOfControlRegistration(serviceRegistry));
		}
    }
}
