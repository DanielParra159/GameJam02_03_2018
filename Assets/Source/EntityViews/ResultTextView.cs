using Source.Component;
using Svelto.ECS;

namespace RockPaperScissors
{
    public class ResultTextView: EntityView
    {
        public ITextComponent TextComponent;
        public ILerpAlphaColorComponent LerpAlphaColorComponent;
    }
}