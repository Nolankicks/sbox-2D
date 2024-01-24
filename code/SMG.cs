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
	[Property] public long ammo { get; set; }
	TimeSince timeSinceShoot;
	[Property] public bool ableShoot {get; set;} = true; 
	[Property] public long maxAmmo { get; set; }
	public TimeSince timeSinceReload;
	protected override void OnUpdate()
	{
		if (attack.PistolGunEnabled)
		{
			ableShoot = false;
		}
		else
		{
			ableShoot = true;
		}
		if (ammo > maxAmmo)
		{
			ammo = maxAmmo;
		}
		if (Input.Down("attack1") && attack.SmgGunEnabled && ableShoot)
		{
			Shoot();
			playerAnimation.Target.Set("b_attack", true);
			attack.pistol.Enabled = false;
			ammo -= 1;
		}
		Log.Info(ammo);
		if (ammo <= 0)
		{
			ammo = 0;
		}
		if (ammo == 0)
		{
		attack.SmgGunEnabled = false;
		}
		
		if (ammo == 60)
		{
			ableShoot = false;
		if (Input.Pressed("reload") && attack.HasGunSmg && ammo == 60 )
		{
			timeSinceReload = 0;
			playerAnimation.Target.Set("b_reload", true);
			ammo -= 1;
			ableShoot = true;
		}

		}

	}

	public void Shoot()
	{
		if (ammo > 0 && ableShoot)
		{
		playerAnimation.HoldType = CitizenAnimationHelper.HoldTypes.Rifle;
		if (timeSinceShoot < 0.1f) return;
		timeSinceShoot = 0;
		var camFoward = body.Transform.Rotation;
		
		var tr = Scene.Trace.Ray(body.Transform.Position, body.Transform.Position + camFoward.Forward * 5000).WithoutTags("player").Run();

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
}
