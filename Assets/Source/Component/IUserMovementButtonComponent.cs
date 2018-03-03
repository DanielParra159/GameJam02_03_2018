using System;

namespace RockPaperScissors
{
    public interface IUserMovementButtonComponent : IComponent
    {
        Action OnPressed { get; set; }
    }
}