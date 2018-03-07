using System.Collections;
using System.Collections.Generic;
using Svelto.DataStructures;
using Svelto.ECS;
using Svelto.Tasks;
using UnityEngine;

namespace RockPaperScissors.Engines
{
    public class LocalUserMovementEngine : MultiEntityViewsEngine<ButtonEntityView, LocalUserView>, IQueryingEntityViewEngine
    {
        private ISequencer _localUserMovementSequence;

        public IEntityViewsDB entityViewsDB { get; set; }
        
        public LocalUserMovementEngine(ISequencer localUserMovementSequence)
        {
            _localUserMovementSequence = localUserMovementSequence;
        }
        
        public void Ready()
        {
        }


        protected override void Add(ButtonEntityView entityView)
        {
            entityView.UserMovementButtonComponent.OnPressed.NotifyOnValueSet(OnPressed);
        }

        protected override void Remove(ButtonEntityView entityView)
        {
            entityView.UserMovementButtonComponent.OnPressed.StopNotify(OnPressed);
        }

        protected override void Add(LocalUserView entityView)
        {
        }

        protected override void Remove(LocalUserView entityView)
        {
        }
        
        private void OnPressed(int entity, UserMovementInfo userMovementInfo)
        {
            FasterReadOnlyList<ButtonEntityView> buttonEntityViews = entityViewsDB.QueryEntityViews<ButtonEntityView>();
            userMovementInfo.entityID = entityViewsDB.QueryEntityViews<LocalUserView>()[0].ID;

            for (int i = 0; i < buttonEntityViews.Count; ++i)
            {
                buttonEntityViews[i].UserMovementButtonComponent.IsInteractable = false;
            }
            
            _localUserMovementSequence.Next(this, ref userMovementInfo);
        }


    }
}