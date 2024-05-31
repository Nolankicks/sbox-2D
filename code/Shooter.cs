using System;
using System.Linq;
using Microsoft.CSharp.RuntimeBinder;
using Sandbox;
using Sandbox.Citizen;

public sealed class Shooter : Component
{
	public TimeSince timeSinceShoot;
	[Property] public GameObject bullet {get; set;}
	[Property] public CitizenAnimationHelper animationHelper {get; set;}
	[Property] public GameObject body {get; set;}
	PlayerController player;
	protected override void OnStart()
	{
		player = Scene.GetAllComponents<PlayerController>().FirstOrDefault();
		timeSinceShoot = 0;
	}
	protected override void OnFixedUpdate()
	{
	var targetRot = Rotation.LookAt(player.Transform.Position.WithZ(Transform.Position.z) - body.Transform.Position);
	body.Transform.Rotation = Rotation.Slerp(body.Transform.Rotation, targetRot, Time.Delta * 5.0f);
	animationHelper.HoldType = CitizenAnimationHelper.HoldTypes.Pistol;
		if (timeSinceShoot > 1f)
		{
			timeSinceShoot = 0;
			Shoot();
			animationHelper.Target.Set("b_attack", true);
			

		}

	}

	void Shoot()
	{
		var tr = Scene.Trace.Ray(body.Transform.Position + Vector3.Up * 55, body.Transform.Position + body.Transform.Rotation.Forward * 1000).WithoutTags("bad").Run();
		if (tr.Hit && tr.GameObject.Parent.Components.TryGet<PlayerController>(out var player, FindMode.EverythingInSelfAndDescendants))
		{
			player.TakeDamage(10);
		}
	}
}
