using System.Linq;
using Sandbox;

public sealed class TerryManager : Component
{
	public PlayerController player;

	protected override void OnAwake()
	{
		player = Scene.GetAllComponents<PlayerController>().FirstOrDefault();
	}
	
	protected override void OnUpdate()
	{
		
		GameObject.Transform.Rotation = Rotation.LookAt(player.GameObject.Transform.Position - GameObject.Transform.Position);
	}
}
