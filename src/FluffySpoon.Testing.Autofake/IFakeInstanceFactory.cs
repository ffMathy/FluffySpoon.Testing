using System;
using System.Collections.Generic;
using System.Text;

namespace FluffySpoon.Testing.Autofake
{
    public interface IFakeInstanceFactory
    {
		Type Type { get; }
		Func<object> Accessor { get; }
    }
}
