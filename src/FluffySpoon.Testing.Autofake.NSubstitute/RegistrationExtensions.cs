using FluffySpoon.Testing.Autofake.NSubstitute;

namespace FluffySpoon.Testing.Autofake
{
	public static class RegistrationExtensions
    {
		public static void UseNSubstitute(
			this Autofaker autofaker)
		{
			autofaker.Configure(new NSubstituteFakeGenerator());
		}
    }
}
