using System.Linq;
using Sandbox;

public sealed class BadGuy : Component
{
	[Property] public GameObject ImpactEffect { get; set; }
	protected override void OnUpdate()
	{
		
	}
	
	[ActionGraphNode("Attack Player"), Pure]
	public static PlayerController GetPlayer()
	{
		return Game.ActiveScene.GetAllComponents<PlayerController>().FirstOrDefault();
	}


}
