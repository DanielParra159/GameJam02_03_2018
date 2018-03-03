using System;
using Svelto.ECS;

namespace RockPaperScissors
{
    public interface IUserMovementButtonComponent : IComponent
    {
        Action<UserMovementInfo> OnPressed { get; set; }
    }

    public struct UserMovementInfo : IEntityStruct
    {
        public int ID { get; set; }
        public UserMovement userMovement { get; private set; }
        public int userId { get; set; }

        public UserMovementInfo(UserMovement userMovement) : this()
        {
            this.userMovement = userMovement;
        }

    }

    public enum UserMovement
    {
        None,
        Rock,
        Scissors,
        Paper
    }
}