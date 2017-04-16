using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using FluffySpoon.Testing.Autofake.Tests.Data;
using NSubstitute;

namespace FluffySpoon.Testing.Autofake.Tests
{
    [TestClass]
    public class AutofacNSubstituteTest
    {
        [TestMethod]
        public void Test()
        {
			var builder = new ContainerBuilder();
			builder.RegisterType<MainModel>().As<IMainModel>();

			var faker = new Autofaker();
			faker.UseNSubstitute();
			faker.UseAutofac(builder);

			faker.RegisterFakesForConstructorParameterTypesOf<IMainModel>();

			var container = builder.Build();

			var fakeFirstDependency = container.Resolve<IFirstDependencyModel>();
			fakeFirstDependency.SayFoo().Returns("fakefoo");

			var fakeSecondDependency = container.Resolve<ISecondDependencyModel>();
			fakeSecondDependency.SayBar().Returns("fakebar");

			var mainModel = container.Resolve<IMainModel>();
			Assert.AreEqual("fakefoofakebar", mainModel.SayStuff());
		}
    }
}
