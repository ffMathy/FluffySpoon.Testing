using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FluffySpoon.Autofake
{
    public interface IInversionOfControlRegistration
    {
		void RegisterInterfaceTypeAsInstance<TInterface>(
			object instance);
    }
}
