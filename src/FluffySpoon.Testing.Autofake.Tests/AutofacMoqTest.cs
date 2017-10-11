using Microsoft.VisualStudio.TestTools.UnitTesting;
using Autofac;
using FluffySpoon.Testing.Autofake.Tests.Data;
using NSubstitute;
using Moq;

namespace FluffySpoon.Testing.Autofake.Tests
{
	[TestClass]
    public class AutofacMoqTest
    {
        [TestMethod]
        public void Test()
        {
			var builder = new ContainerBuilder();
			builder.RegisterType<MainModel>().As<IMainModel>();

			var faker = new Autofaker();
			faker.UseMoq();
			faker.UseAutofac(builder);

			faker.RegisterFakesForConstructorParameterTypesOf<IMainModel>();

			var container = builder.Build();

			var fakeFirstDependencyMock = container.Resolve<Mock<IFirstDependencyModel>>();
			var fakeSecondDependencyMock = container.Resolve<Mock<ISecondDependencyModel>>();

			var fakeFirstDependencyMockInterface = container.Resolve<Mock<IFirstDependencyModel>>();
			var fakeSecondDependencyMockInterface = container.Resolve<Mock<ISecondDependencyModel>>();

			Assert.AreSame(fakeFirstDependencyMock, fakeFirstDependencyMockInterface);
			Assert.AreSame(fakeSecondDependencyMock, fakeSecondDependencyMockInterface);

			fakeFirstDependencyMock.Setup(x => x.SayFoo()).Returns("fakefoo");
			fakeSecondDependencyMock.Setup(x => x.SayBar()).Returns("fakebar");

			var fakeFirstDependencyInstance = container.Resolve<IFirstDependencyModel>();
			Assert.AreEqual("fakefoo", fakeFirstDependencyInstance.SayFoo());

			var fakeSecondDependencyInstance = container.Resolve<ISecondDependencyModel>();
			Assert.AreEqual("fakebar", fakeSecondDependencyInstance.SayBar());

			var mainModel = container.Resolve<IMainModel>();
			Assert.AreEqual("fakefoofakebar", mainModel.SayStuff());
		}
    }
}
