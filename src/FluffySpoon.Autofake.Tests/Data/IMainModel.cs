namespace FluffySpoon.Autofake.Tests.Data
{
	public interface IMainModel
	{
		IFirstDependencyModel GetFirstDependency();
		ISecondDependencyModel GetSecondDependency();

		string SayStuff();
	}
}