using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FluffySpoon.Testing.Autofake
{
    public interface IInversionOfControlRegistration
    {
		void RegisterInterfaceTypeAsInstance<TInterface>(
			object instance);
    }
}
