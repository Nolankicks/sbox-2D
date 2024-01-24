using System.Data;
using Sandbox;
using Sandbox.Citizen;

public sealed class SMG : Component
{
	[Property] public GameObject impactEffect { get; set; }
	[Property] public CitizenAnimationHelper playerAnimation { get; set; }
	[Property] public SoundEvent shootSound { get; set; }
	[Property] public GameObject body { get; set; }
	[Property] public Attack attack { get; set; }
	TimeSince timeSinceShoot;
	protected override void OnUpdate()
	{
		if (Input.Down("attack2") && attack.HasGunSmg)
		{
			Shoot();
			playerAnimation.Target.Set("b_attack", true);
		}
	}

	public void Shoot()
	{
		playerAnimation.HoldType = CitizenAnimationHelper.HoldTypes.Rifle;
		if (timeSinceShoot < 0.1f) return;
		timeSinceShoot = 0;
		var camFoward = body.Transform.Rotation.Forward;
		
		var tr = Scene.Trace.Ray(camFoward, camFoward * 5000).WithoutTags("player").Run();

		if (!tr.Hit) return;


		if (tr.Hit)
		{
			Log.Info("Hit");
			var trgo = tr.GameObject;
			Sound.Play(shootSound);
			if (trgo.Tags.Has("bad"))
			{
				trgo.Destroy();
				var deathSound = attack.deathSound;
				Sound.Play(deathSound, tr.HitPosition);
				var deathParticle = attack.particleEffect;
				deathParticle.Clone(new Transform(tr.HitPosition + Vector3.Up * 45, Rotation.LookAt(tr.Normal)));
				attack.manager.AddScore();
				var ragdoll = attack.ragdoll;
				
			}
		}
		else
		{
			Log.Info("Miss");
		}
		if (impactEffect is not null)
		{
			impactEffect.Clone(new Transform(tr.HitPosition + Vector3.Up * 45, Rotation.LookAt(tr.Normal)));
		}

	}
}
