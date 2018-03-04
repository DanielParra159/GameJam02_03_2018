using System;
using Svelto.ECS;

namespace RockPaperScissors
{
    public interface IUserMovementButtonComponent : IComponent
    {
        Action<UserMovementInfo> OnPressed { get; set; }
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
        None,
        Rock,
        Paper,
        Scissors
    }
}