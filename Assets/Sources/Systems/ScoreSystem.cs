using Entitas;
using UnityEngine;

public sealed class ScoreSystem : IInitializeSystem, IExecuteSystem
{
	readonly GameContext _context;

	public ScoreSystem(Contexts contexts)
	{
		_context = contexts.game;
	}
	
	public void Initialize()
	{
		//_context.SetScore(0);
	}

	public void Execute()
	{
		if (Input.GetMouseButtonDown(0))
		{
		//	_context.ReplaceScore(_context.score.value + 1);
		}
	}


}
