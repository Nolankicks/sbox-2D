using System.Linq;
using Sandbox;

public sealed class TerryManager : Component
{
	[Property] public SkinnedModelRenderer body {get; set;}
	[Property] public Shooter shooter {get; set;}
	protected override void OnUpdate()
	{
		
		
		if (Transform.Rotation != body.Transform.Rotation)
		{
			Transform.Rotation = body.Transform.Rotation * Rotation.FromYaw(-180);
		}
	}
}
