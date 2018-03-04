using System.Collections;
using System.Collections.Generic;
using Svelto.DataStructures;
using Svelto.ECS;
using Svelto.Tasks;
using UnityEngine;

namespace RockPaperScissors.Engines
{
    public class LocalUserMovementEngine : MultiEntityViewsEngine<ButtonEntityView, LocalUserView>
    {
        private ISequencer _localUserMovementSequence;
        private LocalUserView _entityView;
        private List<ButtonEntityView> _buttonEntityViews;

        public LocalUserMovementEngine(ISequencer localUserMovementSequence)
        {
            _localUserMovementSequence = localUserMovementSequence;
            _buttonEntityViews = new List<ButtonEntityView>(3);
        }

        protected override void Add(ButtonEntityView entityView)
        {
            _buttonEntityViews.Add(entityView);
            entityView.UserMovementButtonComponent.OnPressed += OnPressed;
        }

        protected override void Remove(ButtonEntityView entityView)
        {
            _buttonEntityViews.Remove(entityView);
            entityView.UserMovementButtonComponent.OnPressed -= OnPressed;
        }

        protected override void Add(LocalUserView entityView)
        {
            _entityView = entityView;
        }

        protected override void Remove(LocalUserView entityView)
        {
            _entityView = null;
        }

        private void OnPressed(UserMovementInfo userMovementInfo)
        {
            userMovementInfo.entityID = _entityView.ID;

            for (int i = 0; i < _buttonEntityViews.Count; ++i)
            {
                _buttonEntityViews[i].UserMovementButtonComponent.IsInteractable = false;
            }
            
            _localUserMovementSequence.Next(this, ref userMovementInfo);
        }
    }
}