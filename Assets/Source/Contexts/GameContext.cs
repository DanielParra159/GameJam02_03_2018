using System.Collections.Generic;
using System.IO;
using RockPaperScissors.DataSources;
using RockPaperScissors.Engines;
using RockPaperScissors.Implementor;
using Svelto.Context;
using Svelto.ECS;
using Svelto.ECS.Schedulers.Unity;
using Svelto.Tasks;
using UnityEngine;

namespace RockPaperScissors
{
    public class Game : ICompositionRoot
    {
        public Game()
        {
            SetupEnginesAndEntities();
        }

        void SetupEnginesAndEntities()
        {
            _enginesRoot = new EnginesRoot(new UnitySumbmissionEntityViewScheduler());

            _entityFactory = _enginesRoot.GenerateEntityFactory();
            var entityFunctions = _enginesRoot.GenerateEntityFunctions();

            GameObjectFactory factory = new GameObjectFactory();

            Sequencer turnSequence = new Sequencer();

            LocalUserMovementEngine localUserMovementEngine = new LocalUserMovementEngine(turnSequence);
            TurnEngine turnEngine = new TurnEngine(turnSequence);
            AIUserMovementEngine aiUserMovementEngine = new AIUserMovementEngine(turnSequence);
            TurnResolutionEngine turnResolutionEngine = new TurnResolutionEngine();

            turnSequence.SetSequence(
                new Steps
                {
                    {
                        localUserMovementEngine,
                        new To
                        {
                            turnEngine
                        }
                    },
                    {
                        turnEngine,
                        new To
                        {
                            {
                                0, new IStep[]
                                   {
                                       aiUserMovementEngine
                                   }
                            },
                            {1, new IStep[] {turnResolutionEngine}}
                        }
                    },
                    {
                        aiUserMovementEngine,
                        new To
                        {
                            turnEngine
                        }
                    }
                }
            );

            _enginesRoot.AddEngine(localUserMovementEngine);
            _enginesRoot.AddEngine(turnEngine);
            _enginesRoot.AddEngine(aiUserMovementEngine);
            _enginesRoot.AddEngine(turnResolutionEngine);
        }

        /// <summary>
        /// This is a standard approach to create Entities from already existing GameObject in the scene
        /// It is absolutely not necessary, but convienent in case you prefer this way
        /// </summary>
        /// <param name="contextHolder"></param>
        void ICompositionRoot.OnContextCreated(UnityContext contextHolder)
        {
            BuildEntitiesFromScene(contextHolder);

            JSonSceneData[] sceneData = ReadSceneData();
            List<IImplementor> implementors = new List<IImplementor>();
            implementors.Add(new LocalUserImplementor());
            implementors.Add(sceneData[0].UserConfig.Player1HandImplementor);
            _entityFactory.BuildEntity<LocalUserEntityDescriptor>(0, implementors.ToArray());

            implementors = new List<IImplementor>();
            implementors.Add(new AIUserImplementor());
            implementors.Add(sceneData[0].UserConfig.Player2HandImplementor);
            _entityFactory.BuildEntity<AIUserEntityDescriptor>(1, implementors.ToArray());

            _entityFactory.BuildEntity<ResultTextDescriptor>(2, new[] {sceneData[0].ResultTextConfig.ResultTextImplementor});

            for (int i = 0; i < sceneData[0].UserConfig.UserMovementButtonImplementors.Length; ++i)
            {
                _entityFactory.BuildEntity<UserMovementButtonEntityDescriptor>(3 + i,
                    new[] {sceneData[0].UserConfig.UserMovementButtonImplementors[i]});
            }
        }

        static JSonSceneData[] ReadSceneData()
        {
            string json = File.ReadAllText(DataConstants.DataPaths.SceneDataPath);

            JSonSceneData[] sceneData = JsonHelper.getJsonArray<JSonSceneData>(json);

            return sceneData;
        }

        void BuildEntitiesFromScene(UnityContext contextHolder)
        {
            //An EntityDescriptorHolder is a special Svelto.ECS class created to exploit
            //GameObjects to dynamically retrieve the Entity information attached to it.
            //Basically a GameObject can be used to hold all the information needed to create
            //an Entity and later queries to build the entitity itself.
            //This allow to trigger a sort of polyformic code that can be re-used to 
            //create several type of entities.

            IEntityDescriptorHolder[] entities = contextHolder.GetComponentsInChildren<IEntityDescriptorHolder>();

            //However this common pattern in Svelto.ECS application exists to automatically
            //create entities from gameobjects already presented in the scene.
            //I still suggest to avoid this method though and create entities always
            //manually. Basically EntityDescriptorHolder should be avoided
            //whenver not strictly necessary.

            for (int i = 0; i < entities.Length; i++)
            {
                var entityDescriptorHolder = entities[i];
                var entityDescriptor = entityDescriptorHolder.RetrieveDescriptor();
                _entityFactory.BuildEntity
                (((MonoBehaviour) entityDescriptorHolder).gameObject.GetInstanceID(),
                    entityDescriptor,
                    (entityDescriptorHolder as MonoBehaviour).GetComponentsInChildren<IImplementor>());
            }
        }

        //part of Svelto.Context
        void ICompositionRoot.OnContextInitialized() {}

        //part of Svelto.Context
        void ICompositionRoot.OnContextDestroyed()
        {
            //final clean up
            _enginesRoot.Dispose();

            //Tasks can run across level loading, so if you don't want
            //that, the runners must be stopped explicitily.
            //carefull because if you don't do it and 
            //unintentionally leave tasks running, you will cause leaks
            TaskRunner.Instance.StopAndCleanupAllDefaultSchedulerTasks();
        }

        EnginesRoot _enginesRoot;
        IEntityFactory _entityFactory;
    }

    public class GameContext : UnityContext<Game> {}
}