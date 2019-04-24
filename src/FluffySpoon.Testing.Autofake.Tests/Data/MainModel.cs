using System.Collections.Generic;

namespace FluffySpoon.Testing.Autofake.Tests.Data
{
	class MainModel : IMainModel
	{
		private readonly IFirstDependencyModel _firstDependency;
		private readonly ISecondDependencyModel _secondDependency;
        private readonly IEnumerable<IFirstDependencyModel> _firstDependencyModels;

        public IEnumerable<IFirstDependencyModel> GetFirstDependencyModels() => _firstDependencyModels;
        public IFirstDependencyModel GetFirstDependency() => _firstDependency;
		public ISecondDependencyModel GetSecondDependency() => _secondDependency;

		public string SayStuff()
		{
			return _firstDependency.SayFoo() + _secondDependency.SayBar();
		}

		public MainModel(
			IFirstDependencyModel firstDependency,
			ISecondDependencyModel secondDependency,
            IEnumerable<IFirstDependencyModel> firstDependencyModels,
            string someString)
		{
			_firstDependency = firstDependency;
			_secondDependency = secondDependency;
            _firstDependencyModels = firstDependencyModels;
        }
    }
}
