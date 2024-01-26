using System;
using System.Linq;
using Sandbox;

public sealed class HealthBoostSpawner : Component
{
	[Property] public GameObject healthBoost {get; set;}
	[Property] public TimeSince timeSinceSpawn {get; set;}
	float GetRandom() => Random.Shared.Float(0, 100);

	protected override void OnFixedUpdate()
	{
		var health = Scene.GetAllComponents<HealthManager>().FirstOrDefault();
		GetRandom();
		var random = GetRandom();
		if (random >= 99.9f && health.healthNumber != health.maxHealth && timeSinceSpawn >= 5)
		{
			var HealthBoostSpawn = healthBoost.Clone();
			HealthBoostSpawn.Transform.Position = new Vector3(0, Random.Shared.Float(-150, 150), 0);
			timeSinceSpawn = 0;
			

		}
	}

		

}
