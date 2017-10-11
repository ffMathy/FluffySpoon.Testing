using FluffySpoon.Testing.Autofake.NSubstitute;

namespace FluffySpoon.Testing.Autofake
{
	public static class RegistrationExtensions
    {
		public static void UseMoq(
			this Autofaker autofaker)
		{
			autofaker.Configure(new MoqFakeGenerator());
		}
    }
}
