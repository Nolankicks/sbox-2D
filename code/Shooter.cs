using System;
using System.Linq;
using Microsoft.CSharp.RuntimeBinder;
using Sandbox;

public sealed class Shooter : Component
{
	public TimeSince timeSinceShoot;
	[Property] public GameObject bullet {get; set;}
	protected override void OnStart()
	{
		timeSinceShoot = 0;
	}
	protected override void OnFixedUpdate()
	{
		

		//GameObject.Transform.Rotation = cc.Transform.Rotation;
		if (timeSinceShoot > 1f)
		{
			timeSinceShoot = 0;
			Shoot();
			
			

		}
	}

	void Shoot()
	{
		timeSinceShoot = 0;
		var bulletGo = bullet.Clone(GameObject.Transform.Position + Vector3.Up * 45f);
		var rb = bulletGo.Components.Get<Rigidbody>();
		rb.Velocity = GameObject.Transform.Rotation.Forward * 500f;
	}
}
