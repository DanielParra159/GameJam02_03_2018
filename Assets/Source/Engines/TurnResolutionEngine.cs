using System.Collections;
using System.Collections.Generic;
using System.IO;
using RockPaperScissors.DataSources;
using RockPaperScissors.Implementor;
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
            _waitToEnableButtons = new WaitForSeconds(ReadGlobalData().AlphaDurationOnTextResult);
        }

        public void Ready() {}

        public void Step(ref UserMovementInfo[] token, int condition)
        {
            string resultText;
            if (token[0].userMovement == token[1].userMovement)
            {
                resultText = DataConstants.ResultTexts.Draw;
            }
            else
            {
                if (token[0].userMovement != UserMovement.None && token[1].userMovement != UserMovement.None)
                {
                    if (_movements[token[0].userMovement] == token[1].userMovement)
                    {
                        resultText = DataConstants.ResultTexts.Win;
                    }
                    else
                    {
                        resultText = DataConstants.ResultTexts.Lose;
                    }
                }
                else
                {
                    resultText = CheckNoneMovement(token);
                }
            }

            ResultTextView resultTextView = entityViewsDB.QueryEntityViews<ResultTextView>()[0];
            resultTextView.TextComponent.SetText = resultText;
            resultTextView.LerpAlphaColorComponent.SetVisibleAndDisappearWhenFinished = true;

            _taskRoutine.Start();
        }

        //The movement only can be None by time out
        private string CheckNoneMovement(UserMovementInfo[] token)
        {
            if (token[0].userMovement != UserMovement.None)
            {
                return DataConstants.ResultTexts.Win;
            }

            return DataConstants.ResultTexts.Lose;
        }

        IEnumerator WaitToEnableButtons()
        {
            yield return _waitToEnableButtons;

            FasterReadOnlyList<UserMovementButtonEntityView> buttonEntityViews = entityViewsDB.QueryEntityViews<UserMovementButtonEntityView>();
            for (int i = 0; i < buttonEntityViews.Count; ++i)
            {
                buttonEntityViews[i].ButtonComponent.IsInteractable = true;
            }
            
            FasterReadOnlyList<HandAnimatorView> handAnimatorView = entityViewsDB.QueryEntityViews<HandAnimatorView>();
            for (int i = 0; i < handAnimatorView.Count; ++i)
            {
                handAnimatorView[i].HandComponent.SetAnimationTrigger = Animations.IdleRandom;
            }
        }

        static JSonGlobalData ReadGlobalData()
        {
            string json = File.ReadAllText(DataConstants.DataPaths.GlobalDataPath);

            JSonGlobalData[] globalData = JsonHelper.getJsonArray<JSonGlobalData>(json);

            return globalData[0];
        }
    }
}