using System;
using Svelto.ECS;

namespace RockPaperScissors
{
    public interface IUserMovementButtonComponent : IComponent
    {
        DispatchOnSet<UserMovementInfo> OnPressed     { get; }
        bool IsInteractable { get; set; }
    }

    public struct UserMovementInfo : IEntityStruct
    {
        public int ID { get; set; }
        public UserMovement userMovement { get; private set; }
        public int entityID { get; set; }

        public UserMovementInfo(UserMovement userMovement) : this()
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