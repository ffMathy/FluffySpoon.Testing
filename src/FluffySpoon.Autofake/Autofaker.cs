using System;
using System.Linq;
using System.Reflection;

namespace FluffySpoon.Autofake
{
    public class Autofaker
    {
		private readonly IInversionOfControlRegistration _registration;
		private readonly IFakeGenerator _fakeGenerator;

		public Autofaker(
			IInversionOfControlRegistration registration,
			IFakeGenerator fakeGenerator)
		{
			_registration = registration;
			_fakeGenerator = fakeGenerator;
		}

		public void RegisterFakesForConstructorTypes<TClassOrInterface>()
		{
			var classType = typeof(TClassOrInterface).GetTypeInfo();
			var interfaceType = classType;

			if(classType.IsInterface)
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
				var argumentType = argument
					.ParameterType
					.GetTypeInfo();
				if (!argumentType.IsInterface)
					continue;
				
				_registration.RegisterTypeAsInstance(
					interfaceType,
					() => _fakeGenerator.GenerateFake(argumentType));
			}
		}

		private TypeInfo FindImplementingClassType(TypeInfo interfaceType)
		{
			var assemblyTypes = interfaceType
				.Assembly
				.GetTypes()
				.Select(x => x.GetTypeInfo());
			
			foreach(var classType in assemblyTypes)
			{
				if (!classType.IsClass)
					continue;

				var implementedInterfaces = classType
					.GetInterfaces()
					.Select(x => x.GetTypeInfo());
				if (!implementedInterfaces.Any(x => x == interfaceType))
					continue;

				return classType;
			}

			throw new InvalidOperationException("Could not find an implementing class of " + interfaceType.FullName + " in the same assembly.");
		}
	}
}
