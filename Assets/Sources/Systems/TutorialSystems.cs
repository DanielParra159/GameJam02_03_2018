using Sources.Systems;

public class TutorialSystems : Feature
{
	public TutorialSystems(Contexts contexts) : base ("Tutorial Systems")
	{
		Add(new HelloWorldSystem(contexts));
		Add(new LogMouseClickSystem(contexts));
		Add(new ScoreSystem(contexts));
		Add(new DebugMessageSystem(contexts));
		Add(new TestButtonConsumeSystem(contexts));
		Add(new CleanupTestButtonSystem(contexts));
		Add(new EventSystems(contexts));
		Add(new CleanupDebugMessageSystem(contexts));
	}
}