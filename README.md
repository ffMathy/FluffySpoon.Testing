Need practise in making a system testable and writing tests? See: https://github.com/ffMathy/testability-kata

# FluffySpoon.Testing.Autofake
The `FluffySpoon.Testing.Autofake` package makes it easy for you to automatically register all your class dependencies as fakes, resulting in a setup that is easier to use with Test Driven Development (TDD) and more flexible to change in your code.

## Sample using `Autofac` for IOC/DI and `NSubstitute` for faking
```csharp
var containerBuilder = new ContainerBuilder();

var faker = new Autofaker();
faker.UseNSubstitute();
faker.UseAutofac(builder);

//MyClassThatIWantToTest contains two constructor parameters of types IDependency1 
//and IDependency2 respectively. these two dependencies will be registered as 
//single-instance fakes in the IOC container.
faker.RegisterFakesForConstructorParameterTypesOf<MyClassThatIWantToTest>();

var container = builder.Build();

//this instance is not faked out, because it is the class that we want to test
var myClassThatIWantToTest = container.Resolve<MyClassThatIWantToTest>();

//these instances are fakes and single-instance, because they are constructor 
//parameters of MyClassThatIWantToTest.
var fakeDependency1 = container.Resolve<IDependency1>();
var fakeDependency2 = container.Resolve<IDependency2>();
```

## Dependency Injection and Inversion of Control systems

### Autofac
**Package:** `FluffySpoon.Testing.Autofake.Autofac`

```csharp
var builder = new ContainerBuilder();

var faker = new Autofaker();
faker.UseAutofac(builder);

faker.RegisterFakesForConstructorParameterTypesOf<MyClassThatIWantToTest>();

var container = builder.Build();
```

### Microsoft's Dependency Injection (used in ASP .NET Core and .NET Core)
**Package:** `FluffySpoon.Testing.Autofake.MicrosoftDependencyInjection`

```csharp
var serviceCollection = new ServiceCollection();

var faker = new Autofaker();
faker.UseMicrosoftDependencyInjection(serviceCollection);

faker.RegisterFakesForConstructorParameterTypesOf<MyClassThatIWantToTest>();

var serviceProvider = serviceCollection.BuildServiceProvider();
```

## Faking frameworks

### NSubstitute
**Package:** `FluffySpoon.Testing.Autofake.NSubstitute`

```csharp
var faker = new Autofaker();
faker.UseNSubstitute();

...

var fakeClassDependency = container.Resolve<IFakeClassDependency>();
fakeClassDependency.SayFoo().Returns("fakefoo");

fakeClassDependency.SayFoo(); //returns "fakefoo"
```

### Moq
**Package:** `FluffySpoon.Testing.Autofake.Moq`

```csharp
var faker = new Autofaker();
faker.UseMoq();

...

var fakeClassDependencyMock = container.Resolve<IMock<IFakeClassDependency>>();
fakeClassDependencyMock.Setup(x => x.SayFoo()).Returns("fakefoo");

var fakeClassDependency = container.Resolve<IFakeClassDependency>();
fakeClassDependency.SayFoo(); //returns "fakefoo"
```
