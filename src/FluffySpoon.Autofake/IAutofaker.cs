namespace FluffySpoon.Autofake
{
	public interface IAutofaker
	{
		void Configure(
			IInversionOfControlRegistration registration, 
			IFakeGenerator fakeGenerator);

		void RegisterFakesForConstructorParameterTypesOf<TClassOrInterface>();
	}
}