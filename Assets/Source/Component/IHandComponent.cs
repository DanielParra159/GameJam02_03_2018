namespace RockPaperScissors.Implementor
{
    public interface IHandComponent
    {
        Animations SetAnimationTrigger { set; }
        UserMovement SetMovementSprite { set; }
    }

    public enum Animations
    {
        Idle,
        IdleRandom
    }
}