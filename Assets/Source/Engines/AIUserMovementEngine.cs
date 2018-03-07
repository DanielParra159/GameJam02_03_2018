using Svelto.ECS;
using UnityEngine;

namespace RockPaperScissors.Engines
{
    public class AIUserMovementEngine : IStep<int>, IQueryingEntityViewEngine
    {
        private ISequencer _sequencer;

        public IEntityViewsDB entityViewsDB { get; set; }
        public void Ready() {}

        public AIUserMovementEngine(ISequencer sequencer)
        {
            _sequencer = sequencer;
        }

        public void Step(ref int token, int condition)
        {
            var userMovementInfo = new UserMovementInfo((UserMovement) (Random.Range(0, 3) + 1));
            userMovementInfo.entityID = entityViewsDB.QueryEntityViews<AIUserView>()[0].ID;
            _sequencer.Next(this, ref userMovementInfo);
        }
    }
}