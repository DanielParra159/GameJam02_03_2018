namespace RockPaperScissors
{
    public interface IButtonComponent : IComponent
    {
        bool IsPressed { get; }
        bool Reset { set; }
    }
}