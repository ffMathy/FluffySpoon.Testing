using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using FluffySpoon.Testing.Autofake.Tests.Data;
using NSubstitute;
using Microsoft.Extensions.DependencyInjection;

namespace FluffySpoon.Testing.Autofake.Tests
{
	[TestClass]
    public class MicrosoftDependencyInjectionNSubstituteTest
	{
        [TestMethod]
        public void Test()
        {
			var serviceCollection = new ServiceCollection();
			serviceCollection.AddTransient(typeof(IMainModel), typeof(MainModel));

			var faker = new Autofaker();
			faker.UseNSubstitute();
			faker.UseMicrosoftDependencyInjection(serviceCollection);

			faker.RegisterFakesForConstructorParameterTypesOf<IMainModel>();
			
			var serviceProvider = serviceCollection.BuildServiceProvider();

			var fakeFirstDependency = serviceProvider.GetService<IFirstDependencyModel>();
			fakeFirstDependency.SayFoo().Returns("fakefoo");

			var fakeSecondDependency = serviceProvider.GetService<ISecondDependencyModel>();
			fakeSecondDependency.SayBar().Returns("fakebar");

			var mainModel = serviceProvider.GetService<IMainModel>();
			Assert.AreEqual("fakefoofakebar", mainModel.SayStuff());
		}
    }
}
