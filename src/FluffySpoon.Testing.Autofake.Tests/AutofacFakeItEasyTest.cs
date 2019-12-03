using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using FakeItEasy;
using FluffySpoon.Testing.Autofake.Tests.Data;
using NSubstitute;

namespace FluffySpoon.Testing.Autofake.Tests
{
	[TestClass]
    public class AutofacFakeItEasyTest
    {
        [TestMethod]
        public void Test()
        {
			var builder = new ContainerBuilder();
			builder.RegisterType<MainModel>().As<IMainModel>();

			var faker = new Autofaker();
			faker.UseFakeItEasy();
			faker.UseAutofac(builder);

			faker.RegisterFakesForConstructorParameterTypesOf<IMainModel>();

			var container = builder.Build();

            var fakeEnumerable = container
                .Resolve<IEnumerable<IFirstDependencyModel>>()
                .ToArray();
            Assert.AreEqual(1, fakeEnumerable.Length);

            var fakeEnumerableInstance = fakeEnumerable.Single();

            var fakeFirstDependency = container.Resolve<IFirstDependencyModel>();
			A.CallTo(() => fakeFirstDependency.SayFoo()).Returns("fakefoo");

            Assert.AreSame(fakeFirstDependency, fakeEnumerableInstance);

			var fakeSecondDependency = container.Resolve<ISecondDependencyModel>();
            A.CallTo(() => fakeSecondDependency.SayBar()).Returns("fakebar");

			var mainModel = container.Resolve<IMainModel>();
			Assert.AreEqual("fakefoofakebar", mainModel.SayStuff());
		}
    }
}
