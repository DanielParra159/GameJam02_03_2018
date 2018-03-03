using Svelto.ECS;
using UnityEngine;

namespace RockPaperScissors.Engines
{
    public class TurnEngine : IQueryingEntityViewEngine, IStep<UserMovementInfo>
    {
        public IEntityViewsDB entityViewsDB { get; set; }

        public void Ready()
        {
        }

        public TurnEngine()
        {
        }


        public void Step(ref UserMovementInfo token, int condition)
        {
            TurnEntityView user = entityViewsDB.QueryEntityView<TurnEntityView>(token.userId);
            Debug.Log(token.userMovement);
        }
    }
}