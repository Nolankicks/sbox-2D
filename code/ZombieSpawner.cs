using System;
using System.Linq;
using Sandbox;

public sealed class ZombieSpawner : Component
{
	[Property] public GameObject ZombiePrefab { get; set; }
	public float GetRandom() => Random.Shared.Float(1, 100);
	
	protected override void OnUpdate()
	{

	}
	void SpawnZombie()
	{
		var spawn = Scene.Components.GetAll<SpawnPoint>();
		var cc = Scene.Components.GetAll<PlayerController>().FirstOrDefault();
		var zombieGo = ZombiePrefab.Clone();
		zombieGo.Transform.Position = new Vector3(0, Random.Shared.Float(-200, 200), 0);
		zombieGo.Transform.Rotation = Rotation.FromYaw(180);
		
		zombieGo.Enabled = true;
	}
	TimeUntil nextSecond = 0f;
	protected override void OnFixedUpdate()
	{
		if (nextSecond)
		{
			var random = GetRandom();
			GetRandom();

			if (random >= 60f)
			{
				SpawnZombie();
			}
			nextSecond = 1;
			Log.Info(random);
		}
		
		
	}
}
