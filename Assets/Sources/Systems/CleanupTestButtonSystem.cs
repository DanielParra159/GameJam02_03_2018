using Entitas;

public class CleanupTestButtonSystem : ICleanupSystem
{
	readonly GameContext _context;
	readonly IGroup<GameEntity> _sourcesComponentsTestButtonConsume;

	public CleanupTestButtonSystem(Contexts contexts)
	{
		_context = contexts.game;
		_sourcesComponentsTestButtonConsume = _context.GetGroup(GameMatcher.TestButtonConsume);
	}

	public void Cleanup()
	{
		foreach (var e in _sourcesComponentsTestButtonConsume.GetEntities())
		{
			e.Destroy();
		}
	}
}