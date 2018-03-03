using System.Collections.Generic;
using Entitas;

namespace Sources.Systems
{
    public class TestButtonConsumeSystem: ReactiveSystem<GameEntity>, IInitializeSystem
    {
        readonly GameContext _context;
        
        public TestButtonConsumeSystem(Contexts contexts) : base(contexts.game)
        {
            _context = contexts.game;
        }

        public void Initialize()
        {
            _context.SetScore(0);
        }
        
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.TestButtonConsume.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasTestButtonConsume;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var e in entities)
            { 
                _context.ReplaceScore(_context.score.value + 1);
            }
        }
    }
}