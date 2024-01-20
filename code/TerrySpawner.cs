using System;
using System.IO;
using System.Security.Cryptography;
using Sandbox;

public sealed class TerrySpawner : Component
{
	[Property] public GameObject terry {get; set;}
	public TimeSince timeSinceSpawn {get; set;}
	[Property] public Rotation rotation {get; set;}

	protected override void OnUpdate()
	{
		if (timeSinceSpawn > 6f)
		{
			SpawnTerry();
		}
	}
	void SpawnTerry()
	{
		var terryGo = terry.Clone();
		terryGo.Transform.Position = new Vector3(0, Random.Shared.Float(-200, 200), 0);
		terryGo.Transform.Rotation = rotation;
		timeSinceSpawn = 0;
	}
}
