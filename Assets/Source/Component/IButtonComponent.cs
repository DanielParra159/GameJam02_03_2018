using System;

namespace RockPaperScissors
{
    public interface IButtonComponent : IComponent
    {
        Action OnPressed { get; set; }
    }
}