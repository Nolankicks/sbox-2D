using System;
using Sandbox;

public sealed class HealthBoostSpawner : Component
{
	[Property] public GameObject healthBoost {get; set;}
	[Property] public TimeSince timeSinceSpawn {get; set;}
	float GetRandom() => Random.Shared.Float(0, 100);

	protected override void OnFixedUpdate()
	{
		GetRandom();
		var random = GetRandom();
		if (random >= 99.9f)
		{
			var HealthBoostSpawn = healthBoost.Clone();
			HealthBoostSpawn.Transform.Position = new Vector3(0, Random.Shared.Float(-150, 150), 0);
			timeSinceSpawn = 0;
			

		}
	}

		

}
