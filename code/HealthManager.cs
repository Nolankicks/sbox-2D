using System;
using Sandbox;

public sealed class HealthManager : Component
{
	public float healthNumber = 100;
	[Property] public Manager manager {get; set;}
	[Property] public SceneFile menu { get; set; }
	
	protected override void OnFixedUpdate()
	{
		// /Log.Info(score);

		if (healthNumber == 0)
		{
			manager.EndGame();
		}
	}

}
