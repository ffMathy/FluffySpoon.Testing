using System;
using System.Collections.Generic;
using System.Text;

namespace FluffySpoon.Testing.Autofake
{
    public class FakeInstanceFactory: IFakeInstanceFactory
    {
		public FakeInstanceFactory(
			Type type,
			Func<object> accessor)
		{
			Type = type;
			Accessor = accessor;
		}

		public Type Type { get; }
		public Func<object> Accessor { get; }
	}
}
