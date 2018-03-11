using Svelto.ECS;

namespace RockPaperScissors
{
    public interface IButtonComponent : IComponent
    {
        DispatchOnSet<bool> OnPressed     { get; }
        bool IsInteractable { get; set; }
    }

    public interface IUserMovementButtonComponent : IComponent
    {
        UserMovementInfo UserMovementInfo { get; }
    }
    

    public class UserMovementInfo : IEntityStruct
    {
        public int ID { get; set; }
        public UserMovement userMovement { get; private set; }
        public int entityID { get; set; }

        public UserMovementInfo(UserMovement userMovement)
        {
            this.userMovement = userMovement;
        }

    }

    public enum UserMovement
    {
        None = -1,
        Rock = 0,
        Paper,
        Scissors
    }
}