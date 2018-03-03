using Entitas;
using Entitas.CodeGeneration.Attributes;

[Falles, Unique, Event(false)]
public class MyTestComponent: IComponent 
{    
	public string fallera;
	public int algo;
}