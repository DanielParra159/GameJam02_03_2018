using RockPaperScissors;

namespace Source.Component
{
    public interface ILerpAlphaColorComponent : IComponent
    {
        bool SetVisibleAndDisappearWhenFinished { set; }
    }
}