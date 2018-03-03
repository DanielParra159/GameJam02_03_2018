using System.Collections;
using Svelto.DataStructures;
using Svelto.ECS;
using Svelto.Tasks;
using UnityEngine;

namespace RockPaperScissors.Engines
{
    public class UserMovementEngine : SingleEntityViewEngine<ButtonEntityView>, IQueryingEntityViewEngine
    {
        public IEntityViewsDB entityViewsDB { set; private get; }

        public void Ready()
        {
        }

        public UserMovementEngine(ISequencer enemyrDamageSequence)
        {
        }

        protected override void Add(ButtonEntityView entityView)
        {
            entityView.buttonComponent.OnPressed += OnPressed;
        }

        protected override void Remove(ButtonEntityView entityView)
        {
            entityView.buttonComponent.OnPressed -= OnPressed;
        }

        private void OnPressed()
        {
            Debug.Log("ok");
        }
    }
}