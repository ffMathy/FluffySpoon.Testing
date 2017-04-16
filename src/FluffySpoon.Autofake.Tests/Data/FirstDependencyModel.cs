using System;
using System.Collections.Generic;
using System.Text;

namespace FluffySpoon.Autofake.Tests.Data
{
    class FirstDependencyModel : IFirstDependencyModel
	{
		public string SayFoo()
		{
			return "foo";
		}
    }
}
