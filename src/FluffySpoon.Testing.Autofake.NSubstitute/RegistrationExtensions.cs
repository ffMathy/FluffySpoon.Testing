using FluffySpoon.Testing.Autofake.NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

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
