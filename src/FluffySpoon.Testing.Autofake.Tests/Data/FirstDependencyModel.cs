using System;
using System.Collections.Generic;
using System.Text;

namespace FluffySpoon.Testing.Autofake.Tests.Data
{
    class FirstDependencyModel : IFirstDependencyModel
	{
		public string SayFoo()
		{
			return "foo";
		}
    }
}
