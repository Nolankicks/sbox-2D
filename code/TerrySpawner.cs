using System;
using System.IO;
using System.Linq;

using Sandbox;

public sealed class TerrySpawner : Component
{
	[Property] public GameObject terry {get; set;}
	public float GetRandom() => Random.Shared.Float(1, 100);
	public TimeSince timeSinceSpawn {get; set;}


	protected override void OnUpdate()
	{
		var cc = Scene.Components.GetAll<CharacterController>();
		
	}
	void SpawnTerry()
	{
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
	GetRandom();
    var random = GetRandom();
		if (random >= 50f)
		{
			SpawnTerry();
		}
    nextSecond = 1;
	Log.Info(random);
  }
}
}
