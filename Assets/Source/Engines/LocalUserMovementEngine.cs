using Svelto.DataStructures;
using Svelto.ECS;

namespace RockPaperScissors.Engines
{
    public class LocalUserMovementEngine : SingleEntityViewEngine<UserMovementButtonEntityView>, IQueryingEntityViewEngine
    {
        private ISequencer _localUserMovementSequence;

        public IEntityViewsDB entityViewsDB { get; set; }

        public LocalUserMovementEngine(ISequencer localUserMovementSequence)
        {
            _localUserMovementSequence = localUserMovementSequence;
        }

        public void Ready() {}

        protected override void Add(UserMovementButtonEntityView entityView)
        {
            entityView.ButtonComponent.OnPressed.NotifyOnValueSet(OnPressed);
        }

        protected override void Remove(UserMovementButtonEntityView entityView)
        {
            entityView.ButtonComponent.OnPressed.StopNotify(OnPressed);
        }

        private void OnPressed(int entity, bool pressed)
        {
            FasterReadOnlyList<UserMovementButtonEntityView> buttonEntityViews = entityViewsDB.QueryEntityViews<UserMovementButtonEntityView>();
            int ID = 0;
            UserMovementInfo userMovementInfo = buttonEntityViews[ID].UserMovementButtonComponent.UserMovementInfo;
            userMovementInfo.entityID = ID;

            for (int i = 0; i < buttonEntityViews.Count; ++i)
            {
                buttonEntityViews[i].ButtonComponent.IsInteractable = false;
            }

            _localUserMovementSequence.Next(this, ref userMovementInfo);
        }
    }
}