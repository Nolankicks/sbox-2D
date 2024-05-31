using System.Data;
using System.Linq;
using Sandbox;
using Sandbox.Citizen;

public sealed class SMG : Component
{
	[Property] public GameObject impactEffect { get; set; }
	public CitizenAnimationHelper playerAnimation { get; set; }
	[Property] public SoundEvent shootSound { get; set; }
	public GameObject body { get; set; }
	[Property] public long ammo { get; set; }
	public TimeSince timeSinceShoot = 0;
	[Property] public bool ableShoot {get; set;} = true;
	public PlayerController controller { get; set; }
	[Property] public long maxAmmo { get; set; }
	public TimeSince timeSinceReload;
	protected override void OnUpdate()
	{
		controller = Scene.GetAllComponents<PlayerController>().FirstOrDefault( x => !x.IsProxy);
		if (controller is null) return;
		playerAnimation = controller.animationHelper;
		body = controller.body.GameObject;
		if (playerAnimation is null || body is null) return;
		if (ammo > maxAmmo)
		{
			ammo = maxAmmo;
		}
		if (Input.Down("attack1") && ammo > 0)
		{
			Shoot();
		}
		if (ammo <= 0)
		{
			GameObject.Destroy();
		}
	}

	public void Shoot()
	{
		if (ammo <= 0 || timeSinceShoot <= 0.1f) return;
		ammo -= 1;
		playerAnimation.Target.Set("b_attack", true);
		playerAnimation.HoldType = CitizenAnimationHelper.HoldTypes.Rifle;
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
				//var deathSound = attack.deathSound;
				//Sound.Play(deathSound, tr.HitPosition);
				//var deathParticle = attack.particleEffect;
				//deathParticle.Clone(new Transform(tr.HitPosition + Vector3.Up * 45, Rotation.LookAt(tr.Normal)));
				//attack.manager.AddScore();
				//var ragdoll = attack.humanRagdoll;
				
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
