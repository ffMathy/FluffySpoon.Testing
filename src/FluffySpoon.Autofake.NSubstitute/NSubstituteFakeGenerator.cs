using NSubstitute;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FluffySpoon.Autofake.NSubstitute
{
	public class NSubstituteFakeGenerator : IFakeGenerator
	{
		public object GenerateFake(Type interfaceType)
		{
			return Substitute.For(
				new Type[] { interfaceType }, 
				new object[0]);
		}
	}
}
