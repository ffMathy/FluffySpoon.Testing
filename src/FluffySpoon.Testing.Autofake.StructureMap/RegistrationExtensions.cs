using FluffySpoon.Testing.Autofake.StructureMap;
using StructureMap;

namespace FluffySpoon.Testing.Autofake
{
	public static class RegistrationExtensions
    {
		public static void UseStructureMap(
			this Autofaker autofaker,
			Container container)
		{
			autofaker.Configure(
				new StructureMapInversionOfControlRegistration(container));
		}
    }
}
