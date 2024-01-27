using System;
using System.Linq;
using Sandbox;

public sealed class DuccSpawner : Component
{
	float GetRandom() => Random.Shared.Float(0, 100);
	[Property] public GameObject duccPowerUp { get; set; }
	[Property] public TimeSince timeSinceSpawn { get; set; }
	Attack attack => Scene.GetAllComponents<Attack>().FirstOrDefault();
	
	
	void SpawnDucc()
	{
		if (!attack.DuccBlasterEnabled && !attack.RPGGunEnabled && !attack.SmgGunEnabled && !attack.PistolGunEnabled)
		{
		var duccGo = duccPowerUp.Clone();
		duccGo.Transform.Position = new Vector3(0, Random.Shared.Float(-200, 200), 50);
		}
	}
	TimeUntil nextSecond = 0f;
	protected override void OnFixedUpdate()
	{
		if (nextSecond)
		{
			var random = GetRandom();
			GetRandom();
			if (random >= 90f)
			{
				SpawnDucc();
			}
			nextSecond = 1f;
		}
	}
}
