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
			if (_registration == null)
			{
				throw new InvalidOperationException("An Inversion of Control registration must be specified.");
			}

			if (_fakeGenerator == null)
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

			var constructors = classType.GetTypeInfo()
			  .DeclaredConstructors
			  .ToArray();
			if (constructors.Length > 1)
			{
				throw new InvalidOperationException("More than one constructor was found for " + classType.FullName + ".");
			}

			var constructor = constructors.Single();

			var arguments = constructor.GetParameters();
			foreach (var argument in arguments)
			{
				var argumentInterfaceType = argument.ParameterType;
				var argumentInterfaceTypeInfo = argumentInterfaceType.GetTypeInfo();
				if (!argumentInterfaceTypeInfo.IsInterface)
					continue;

				var registrationType = _registration.GetType();
				var registerTypeAsInstanceMethod = registrationType.GetRuntimeMethod(
					nameof(_registration.RegisterInterfaceTypeAsInstanceFromAccessor),
					new[] { typeof(Func<object>) });
				foreach (var fakeInstanceFactory in _fakeGenerator.GenerateFakeInstanceFactories(argumentInterfaceType))
				{
					var registerTypeAsInstanceGenericMethod = registerTypeAsInstanceMethod.MakeGenericMethod(fakeInstanceFactory.Type);
					registerTypeAsInstanceGenericMethod.Invoke(
						_registration,
						new object[] { fakeInstanceFactory.Accessor });
				}
			}
		}

		private Type FindImplementingClassType(Type interfaceType)
		{
			var assemblyTypes = interfaceType
				.GetTypeInfo()
				.Assembly
				.DefinedTypes;

			foreach (var classType in assemblyTypes)
			{
				if (!classType.IsClass)
					continue;

				var implementedInterfaces = classType.ImplementedInterfaces;
				if (!implementedInterfaces.Any(x => x == interfaceType))
					continue;

				return classType.AsType();
			}

			throw new InvalidOperationException("Could not find an implementing class of " + interfaceType.FullName + " in the same assembly.");
		}
	}
}
