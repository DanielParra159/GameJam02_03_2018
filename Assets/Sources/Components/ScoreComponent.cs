using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique, Event(false)]
public sealed class ScoreComponent : IComponent {
	public int value;
}
