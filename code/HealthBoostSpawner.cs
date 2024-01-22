using System;
using Sandbox;

public sealed class HealthBoostSpawner : Component
{
	[Property] public GameObject healthBoost {get; set;}
	[Property] public TimeSince timeSinceSpawn {get; set;}

	protected override void OnUpdate()
	{
		if (timeSinceSpawn > 25f)
		{
			var HealthBoostSpawn = healthBoost.Clone();
			HealthBoostSpawn.Transform.Position = new Vector3(0, Random.Shared.Float(-200, 200), 0);
			timeSinceSpawn = 0;

		}
	}
}
