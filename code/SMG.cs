using Sandbox;
using Sandbox.Citizen;

public sealed class SMG : Component
{
	[Property] public GameObject impactEffect { get; set; }
	[Property] public CitizenAnimationHelper playerAnimation { get; set; }
	[Property] public SoundEvent shootSound { get; set; }
	[Property] public GameObject body { get; set; }
	TimeSince timeSinceShoot;
	protected override void OnUpdate()
	{
		if (Input.Down("attack2"))
		{
			Shoot();
			
		}
	}

	public void Shoot()
	{
		if (timeSinceShoot < 0.1f) return;
		timeSinceShoot = 0;
		var camFoward = body.Transform.Rotation.Forward;
		
		var tr = Scene.Trace.Ray(camFoward, camFoward * 5000).WithoutTags("player").Run();

		if (!tr.Hit) return;


		if (tr.Hit)
		{
			Log.Info("Hit");
			var trgo = tr.GameObject;
			//trgo.Destroy();
			Sound.Play(shootSound);
		}
		else
		{
			Log.Info("Miss");
		}
		if (impactEffect is not null)
		{
			impactEffect.Clone(new Transform(tr.HitPosition + Vector3.Up * 64, Rotation.LookAt(tr.Normal)));
		}

	}
}
