namespace RockPaperScissors.Implementor
{
    public interface IHandComponent
    {
        Animations SetAnimationTrigger { set; }
    }

    public enum Animations
    {
        Idle,
        IdleRandom
    }
}