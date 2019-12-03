using FluffySpoon.Testing.Autofake.FakeItEasy;

namespace FluffySpoon.Testing.Autofake
{
	public static class RegistrationExtensions
    {
		public static void UseFakeItEasy(
			this Autofaker autofaker)
		{
			autofaker.Configure(new FakeItEasyFakeGenerator());
		}
    }
}
