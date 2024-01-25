using System;
using System.IO;
using System.Linq;

using Sandbox;

public sealed class TerrySpawner : Component
{
	[Property] public GameObject terry {get; set;}
	public float GetRandom() => Random.Shared.Float(1, 100);
	public TimeSince timeSinceSpawn {get; set;}
	[Property] public Manager manager {get; set;}


	protected override void OnUpdate()
	{
		var cc = Scene.Components.GetAll<CharacterController>();
		
	}
	void SpawnTerry()
	{
		var spawn = Scene.Components.GetAll<SpawnPoint>();
		var cc = Scene.Components.GetAll<PlayerController>().FirstOrDefault();
		var terryGo = terry.Clone();
		terryGo.Transform.Position = new Vector3(0, Random.Shared.Float(-200, 200), 0);
		terryGo.Transform.Rotation = Rotation.FromYaw(180);
		timeSinceSpawn = 0;
		terryGo.Enabled = true;
	}
TimeUntil nextSecond = 0f;
protected override void OnFixedUpdate(){
	
  if(nextSecond){
	var random = GetRandom();
	GetRandom();
	if (500 >= manager.Score)
	{
		if (random >= 50f)
		{
			SpawnTerry();
		}
	}
	if (manager.Score >= 500)	
	{
		if (random >= 45f)
		{
			SpawnTerry();
		}
	}
	if (manager.Score >= 1000)
	{
		if (random >= 40f)
		{
			SpawnTerry();
		}
	}
	if (manager.Score >= 1500)
	{
		if (random >= 35f)
		{
			SpawnTerry();
		}
	}
	if (manager.Score >= 2000)
	{
		if (random >= 35f)
		{
			SpawnTerry();
		}
	}
    nextSecond = 1;
	Log.Info(random);
  }
}
}
