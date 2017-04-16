using System;
using System.Linq;
using System.Reflection;

namespace FluffySpoon.Testing.Autofake
{
    public class Autofaker : IAutofaker
	{
		private IInversionOfControlRegistration _registration;
		private IFakeGenerator _fakeGenerator;

		/// <summary>
		/// Creates an Autofaker instance. Configure with extension methods afterwards.
		/// </summary>
		public Autofaker()
		{
		}

		public Autofaker(
			IInversionOfControlRegistration registration,
			IFakeGenerator fakeGenerator)
		{
			Configure(
				registration,
				fakeGenerator);
		}

		public void Configure(
			IInversionOfControlRegistration registration)
		{
			_registration = registration;
		}

		public void Configure(
			IFakeGenerator fakeGenerator)
		{
			_fakeGenerator = fakeGenerator;
		}

		public void Configure(
			IInversionOfControlRegistration registration,
			IFakeGenerator fakeGenerator)
		{
			Configure(registration);
			Configure(fakeGenerator);
		}

		public void RegisterFakesForConstructorParameterTypesOf<TClassOrInterface>()
		{
			if(_registration == null)
			{
				throw new InvalidOperationException("An inversion of control registration must be specified.");
			}

			if(_fakeGenerator == null)
			{
				throw new InvalidOperationException("A fake generator must be specified.");
			}

			var classType = typeof(TClassOrInterface);
			var interfaceType = classType;

			var classTypeInfo = classType.GetTypeInfo();
			if (classTypeInfo.IsInterface)
			{
				classType = FindImplementingClassType(classType);
			}

			var constructors = classType
				.GetConstructors()
				.ToArray();
			if(constructors.Length > 1)
			{
				throw new InvalidOperationException("More than one constructor was found for " + classType.FullName + ".");
			}

			var constructor = constructors.Single();

			var arguments = constructor.GetParameters();
			foreach(var argument in arguments)
			{
				var argumentInterfaceType = argument.ParameterType;
				var argumentInterfaceTypeInfo = argumentInterfaceType.GetTypeInfo();
				if (!argumentInterfaceTypeInfo.IsInterface)
					continue;
				
				var registrationType = _registration.GetType();
				var registerTypeAsInstanceMethod = registrationType.GetMethod(nameof(_registration.RegisterInterfaceTypeAsInstance));
				var registerTypeAsInstanceGenericMethod = registerTypeAsInstanceMethod.MakeGenericMethod(argumentInterfaceType);
				registerTypeAsInstanceGenericMethod.Invoke(
					_registration,
					new object[] {
						_fakeGenerator.GenerateFake(argumentInterfaceType)
					});
			}
		}

		private Type FindImplementingClassType(Type interfaceType)
		{
			var assemblyTypes = interfaceType
				.GetTypeInfo()
				.Assembly
				.GetTypes();
			
			foreach(var classType in assemblyTypes)
			{
				var classTypeInfo = classType.GetTypeInfo();
				if (!classTypeInfo.IsClass)
					continue;

				var implementedInterfaces = classType
					.GetInterfaces();
				if (!implementedInterfaces.Any(x => x == interfaceType))
					continue;

				return classType;
			}

			throw new InvalidOperationException("Could not find an implementing class of " + interfaceType.FullName + " in the same assembly.");
		}
	}
}
