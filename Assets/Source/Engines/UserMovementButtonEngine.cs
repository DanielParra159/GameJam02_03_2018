using System.Collections;
using Svelto.DataStructures;
using Svelto.ECS;
using Svelto.Tasks;
using UnityEngine;

namespace RockPaperScissors.Engines
{
    public class UserMovementButtonEngine : SingleEntityViewEngine<ButtonEntityView>
    {
        private ISequencer _enemyrDamageSequence;

        public UserMovementButtonEngine(ISequencer enemyrDamageSequence)
        {
            _enemyrDamageSequence = enemyrDamageSequence;
        }

        protected override void Add(ButtonEntityView entityView)
        {
            entityView.UserMovementButtonComponent.OnPressed += OnPressed;
        }

        protected override void Remove(ButtonEntityView entityView)
        {
            entityView.UserMovementButtonComponent.OnPressed -= OnPressed;
        }

        private void OnPressed(UserMovementInfo userMovementInfo)
        {
            Debug.Log("ok");
            //TODO: Send to server
            userMovementInfo.userId = 1; //TODO
            _enemyrDamageSequence.Next(this, ref userMovementInfo);
        }
    }
}