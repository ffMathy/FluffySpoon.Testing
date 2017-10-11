namespace FluffySpoon.Testing.Autofake
{
	public interface IInversionOfControlRegistration
    {
		void RegisterInterfaceTypeAsInstance<TInterface>(
			object instance);
    }
}
