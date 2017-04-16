namespace FluffySpoon.Testing.Autofake.Tests.Data
{
	public interface IMainModel
	{
		IFirstDependencyModel GetFirstDependency();
		ISecondDependencyModel GetSecondDependency();

		string SayStuff();
	}
}