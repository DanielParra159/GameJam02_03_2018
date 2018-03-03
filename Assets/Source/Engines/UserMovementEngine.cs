using System.Collections;
using Svelto.DataStructures;
using Svelto.ECS;
using Svelto.Tasks;
using UnityEngine;

namespace RockPaperScissors.Engines
{
    public class UserMovementEngine: SingleEntityViewEngine<ButtonEntityView>, IQueryingEntityViewEngine
    {
        public IEntityViewsDB entityViewsDB { set; private get; }
        ISequencer            _targetDamageSequence;
        ITaskRoutine          _taskRoutine;

        public void Ready()
        {}

        public UserMovementEngine(ISequencer enemyrDamageSequence)
        {
            _targetDamageSequence = enemyrDamageSequence;
            _taskRoutine = TaskRunner.Instance.AllocateNewTaskRoutine().SetEnumerator(CheckIfButtonIsPressed()).SetScheduler(StandardSchedulers.physicScheduler);
        }

        protected override void Add(ButtonEntityView entityView)
        {
            _taskRoutine.Start();
        }

        protected override void Remove(ButtonEntityView entityView)
        {
            _taskRoutine.Stop();
        }
        
        private IEnumerator CheckIfButtonIsPressed()
        {
            Debug.Log("CheckIfButtonIsPressed");
            while (true)
            {
                FasterReadOnlyList<ButtonEntityView> targetEntitiesView = entityViewsDB.QueryEntityViews<ButtonEntityView>();
                
                if (targetEntitiesView[0].buttonComponent.IsPressed)
                {
                    Debug.Log("Reset");
                    targetEntitiesView[0].buttonComponent.Reset = true;
                }
                yield return null;
            }
           
        }

        
    }
}