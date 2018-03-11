using RockPaperScissors.Engines;
using Svelto.Context;
using Svelto.ECS;
using Svelto.ECS.Schedulers.Unity;
using Svelto.Tasks;

namespace RockPaperScissors {
	public class Menu : ICompositionRoot {
		
		EnginesRoot _enginesRoot;
		IEntityFactory _entityFactory;
		
		public Menu()
        {
            SetupEnginesAndEntities();
        }

        void SetupEnginesAndEntities()
        {
            _enginesRoot = new EnginesRoot(new UnitySumbmissionEntityViewScheduler());

            _entityFactory = _enginesRoot.GenerateEntityFactory();
            IEntityFunctions entityFunctions = _enginesRoot.GenerateEntityFunctions();

            GameObjectFactory factory = new GameObjectFactory();

            Sequencer turnSequence = new Sequencer();

           
        }
		
		public void OnContextCreated(UnityContext contextHolder)
		{
		}

		public void OnContextInitialized()
		{
		}

		public void OnContextDestroyed()
		{
			//final clean up
			_enginesRoot.Dispose();

			//Tasks can run across level loading, so if you don't want
			//that, the runners must be stopped explicitily.
			//carefull because if you don't do it and 
			//unintentionally leave tasks running, you will cause leaks
			TaskRunner.Instance.StopAndCleanupAllDefaultSchedulerTasks();
		}
	}
	public class MenuContext : UnityContext<Menu> {}
}
