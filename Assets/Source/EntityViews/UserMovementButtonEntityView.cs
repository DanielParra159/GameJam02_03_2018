using Svelto.ECS;

namespace RockPaperScissors
{
    public class UserMovementButtonEntityView : EntityView
    {
        public IButtonComponent ButtonComponent;
        public IUserMovementButtonComponent UserMovementButtonComponent;
    }
}