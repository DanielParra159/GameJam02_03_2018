using System.Collections.Generic;
using Svelto.ECS;
using UnityEngine;

namespace RockPaperScissors.Engines
{
    public class TurnResolutionEngine : SingleEntityViewEngine<ButtonEntityView>, IQueryingEntityViewEngine, IStep<UserMovementInfo[]>
    {
        private readonly Dictionary<UserMovement, UserMovement> _movements = new Dictionary<UserMovement, UserMovement>()
        {
            {UserMovement.Rock, UserMovement.Scissors},
            {UserMovement.Paper, UserMovement.Rock},
            {UserMovement.Scissors, UserMovement.Paper},
        };
        private List<ButtonEntityView> _buttonEntityViews;

        public IEntityViewsDB entityViewsDB { get; set; }

        public TurnResolutionEngine()
        {
            _buttonEntityViews = new List<ButtonEntityView>(3);
        }

        public void Ready()
        {
        }

        protected override void Add(ButtonEntityView entityView)
        {
            _buttonEntityViews.Add(entityView);
        }

        protected override void Remove(ButtonEntityView entityView)
        {
            _buttonEntityViews.Remove(entityView);
        }
        
        public void Step(ref UserMovementInfo[] token, int condition)
        {
            if (token[0].userMovement == token[1].userMovement)
            {
                Debug.Log("Empate");
            }
            else
            {
                //TODO: None
                if (_movements[token[0].userMovement] == token[1].userMovement)
                {
                    Debug.Log("Gana usuario 0");
                }
                else
                {
                    Debug.Log("Gana usuario 1");
                }
            }
            
            for (int i = 0; i < _buttonEntityViews.Count; ++i)
            {
                _buttonEntityViews[i].UserMovementButtonComponent.IsInteractable = true;
            }
        }
    }
}