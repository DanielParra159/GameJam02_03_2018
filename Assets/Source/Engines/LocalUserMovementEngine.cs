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
            entityView.ButtonComponent.OnPressed = new DispatchOnSet<bool>(entityView.ID);
            entityView.ButtonComponent.OnPressed.NotifyOnValueSet(OnPressed);
        }

        protected override void Remove(UserMovementButtonEntityView entityView)
        {
            entityView.ButtonComponent.OnPressed.StopNotify(OnPressed);
        }

        private void OnPressed(int entityID, bool pressed)
        {
            UserMovementInfo userMovementInfo = entityViewsDB.QueryEntityView<UserMovementButtonEntityView>(entityID).UserMovementButtonComponent.UserMovementInfo;
            userMovementInfo.entityID = entityViewsDB.QueryEntityViews<LocalUserView>()[0].ID;

            FasterReadOnlyList<UserMovementButtonEntityView> buttonEntityViews = entityViewsDB.QueryEntityViews<UserMovementButtonEntityView>();
            for (int i = 0; i < buttonEntityViews.Count; ++i)
            {
                buttonEntityViews[i].ButtonComponent.IsInteractable = false;
            }

            _localUserMovementSequence.Next(this, ref userMovementInfo);
        }
    }
}