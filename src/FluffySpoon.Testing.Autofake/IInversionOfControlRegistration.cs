using System;

namespace FluffySpoon.Testing.Autofake
{
	public interface IInversionOfControlRegistration
    {
		void RegisterInterfaceTypeAsInstanceFromAccessor<TInterface>(Func<object> instanceAccessor);
    }
}
