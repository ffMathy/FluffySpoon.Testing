using System;
using System.Collections.Generic;
using System.Text;

namespace FluffySpoon.Testing.Autofake.Tests.Data
{
    class MainModel : IMainModel
	{
		private readonly IFirstDependencyModel _firstDependency;
		private readonly ISecondDependencyModel _secondDependency;

		public IFirstDependencyModel GetFirstDependency() => _firstDependency;
		public ISecondDependencyModel GetSecondDependency() => _secondDependency;

		public string SayStuff()
		{
			return _firstDependency.SayFoo() + _secondDependency.SayBar();
		}

		public MainModel(
			IFirstDependencyModel firstDependency,
			ISecondDependencyModel secondDependency)
		{
			_firstDependency = firstDependency;
			_secondDependency = secondDependency;
		}
    }
}
