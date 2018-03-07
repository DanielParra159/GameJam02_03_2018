using System.Collections.Generic;
using Svelto.DataStructures;
using Svelto.ECS;
using UnityEngine;

namespace RockPaperScissors.Engines
{
    public class TurnResolutionEngine : IQueryingEntityViewEngine, IStep<UserMovementInfo[]>
    {
        private readonly Dictionary<UserMovement, UserMovement> _movements = new Dictionary<UserMovement, UserMovement>()
        {
            {UserMovement.Rock, UserMovement.Scissors},
            {UserMovement.Paper, UserMovement.Rock},
            {UserMovement.Scissors, UserMovement.Paper},
        };

        public IEntityViewsDB entityViewsDB { get; set; }

        public void Ready()
        {
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

            FasterReadOnlyList<ButtonEntityView> buttonEntityViews = entityViewsDB.QueryEntityViews<ButtonEntityView>();
            for (int i = 0; i < buttonEntityViews.Count; ++i)
            {
                buttonEntityViews[i].UserMovementButtonComponent.IsInteractable = true;
            }
        }
    }
}