using System.Collections;
using System.Collections.Generic;
using Svelto.DataStructures;
using Svelto.ECS;
using Svelto.Tasks;
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

        private readonly ITaskRoutine _taskRoutine;
        private readonly WaitForSeconds _waitToEnableButtons;
        public IEntityViewsDB entityViewsDB { get; set; }

        public TurnResolutionEngine()
        {
            _taskRoutine = TaskRunner.Instance.AllocateNewTaskRoutine().SetEnumeratorProvider(WaitToEnableButtons);
            _waitToEnableButtons = new WaitForSeconds(3);
        }
        
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

            _taskRoutine.Start();
        }

        IEnumerator WaitToEnableButtons()
        {
            yield return _waitToEnableButtons;
            
            FasterReadOnlyList<ButtonEntityView> buttonEntityViews = entityViewsDB.QueryEntityViews<ButtonEntityView>();
            for (int i = 0; i < buttonEntityViews.Count; ++i)
            {
                buttonEntityViews[i].UserMovementButtonComponent.IsInteractable = true;
            }
        }
    }
}