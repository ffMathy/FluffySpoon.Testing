using System;
using System.Collections.Generic;
using System.Text;

namespace FluffySpoon.Autofake.Tests.Data
{
    class SecondDependencyModel : ISecondDependencyModel
	{
		public string SayBar()
		{
			return "bar";
		}
	}
}
