using RockPaperScissors.Implementor;
using Svelto.ECS;
using UnityEngine;

namespace RockPaperScissors.Engines
{
    public class TurnEngine : IQueryingEntityViewEngine, IStep<UserMovementInfo>
    {
        public IEntityViewsDB entityViewsDB { get; set; }
        private UserMovementInfo[] _userMovementInfo;
        private int _movements;
        private ISequencer _sequencer;

        public void Ready()
        {
        }

        public TurnEngine(ISequencer sequencer)
        {
            _sequencer = sequencer;
            _userMovementInfo = new UserMovementInfo[2];
        }


        public void Step(ref UserMovementInfo token, int condition)
        {
            Debug.Log("User: " + token.entityID + " Movement: " + token.userMovement);

            // xD
            _userMovementInfo[_movements] = token;
            if (++_movements > 1)
            {
                for (int i = 0; i < _userMovementInfo.Length; ++i)
                {
                    HandAnimatorView handAnimatorView = entityViewsDB.QueryEntityView<HandAnimatorView>(_userMovementInfo[i].entityID);
                    handAnimatorView.HandComponent.SetMovementSprite = _userMovementInfo[i].userMovement;
                }
                _movements = 0;
                _sequencer.Next(this, ref _userMovementInfo, 1);
            }
            else
            {
                HandAnimatorView handAnimatorView = entityViewsDB.QueryEntityView<HandAnimatorView>(token.entityID);
                handAnimatorView.HandComponent.SetAnimationTrigger = Animations.IdleRandom;
                int xD = -1;
                _sequencer.Next(this, ref xD, 0);
            }
        }
    }
}