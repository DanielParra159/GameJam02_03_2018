using Svelto.ECS;
using UnityEngine;

namespace RockPaperScissors.Engines
{
    public class AIUserMovementEngine: SingleEntityViewEngine<AIUserView>, IStep<int>
    {
        private AIUserView _entityView;
        private ISequencer _sequencer;

        public AIUserMovementEngine(ISequencer sequencer)
        {
            _sequencer = sequencer;
        }
        protected override void Add(AIUserView entityView)
        {
            _entityView = entityView;
        }

        protected override void Remove(AIUserView entityView)
        {
            _entityView = null;
        }

        public void Step(ref int token, int condition)
        {
            var userMovementInfo = new UserMovementInfo((UserMovement) (Random.Range(0, 3) + 1));
            userMovementInfo.entityID = _entityView.ID;
            _sequencer.Next(this, ref userMovementInfo);
        }
    }
}