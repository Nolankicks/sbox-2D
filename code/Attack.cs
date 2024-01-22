using System.Runtime.InteropServices;
using System.Security.AccessControl;
using Sandbox;
using Sandbox.Citizen;

public sealed class Attack : Component
{
	public TimeSince timeSinceSpawn { get; set; }
	[Property] float Range { get; set; }
	[Property] public GameObject particleEffect {get; set;}
	[Property] GameObject eye {get; set;}
	[Property] SkinnedModelRenderer body {get; set;}
	[Property] CitizenAnimationHelper animationHelper {get; set;}
	[Property] public SoundEvent deathSound {get; set;}
	[Property] SoundEvent soundEvent { get; set; }
	[Property] GameObject ragdoll { get; set; }
	[Property] public Rotation rotation { get; set; }
	[Property] public Manager manager {get; set;}
	[Property] public SoundEvent punchSound {get; set;}
	[Property] public GameObject bullet {get; set;}
	[Property] public GameObject gun {get; set;}
	[Property] public bool HasGun = false;
	[Property] public bool ShowGun = false;
	[Property] float gunRange {get; set;}
	[Property] TimeSince timeSincepowerUp {get; set;}
	[Property] public SoundEvent gunSound {get; set;}
	
	protected override void OnUpdate()
	{
		if (ShowGun)
		{
			gun.Enabled = true;
		}
						if (timeSincepowerUp > 10)
				{
					HasGun = false;
					ShowGun = false;
					gun.Enabled = false;
				}

		var body = Scene.Components.Get<SkinnedModelRenderer>( FindMode.EverythingInDescendants );
		if(Input.Pressed("attack1"))
		{	if (!HasGun)
			{
			Fire();
			animationHelper.Target.Set("b_attack", true);
			Sound.Play(punchSound);
			}
			else
			{
				animationHelper.Target.Set("b_attack", true);
				
				GunPowerUp();
				timeSincepowerUp = 0;
				Sound.Play(gunSound);
			}
	}
	






	void Fire()
	{
		//Perform a trace foward
		animationHelper.HoldType = CitizenAnimationHelper.HoldTypes.Punch;
		var camFoward = animationHelper.EyeWorldTransform.Position;
		var tr = Scene.Trace.Ray(camFoward, camFoward + (camFoward * Range)).WithAnyTags("bad").Run();
		if (tr.Hit)
		{
			var lookDir =  body.Transform.Rotation.Forward;
			timeSinceSpawn = 0;
			manager.AddScore();
			Log.Info("test");
			var trgo = tr.GameObject;
			var trgoPos = trgo.Transform.Position;
			Sound.Play(deathSound);
			Log.Info(trgo.Name);
			trgo.Destroy();
			//var ragdollGo = ragdoll.Clone(trgoPos, rotation);
			particleEffect.Clone(trgoPos);
			
			
		}
	}
}
void GunPowerUp()
{
	var camFoward = animationHelper.EyeWorldTransform.Position;
	var pos = body.Transform.Position + Vector3.Up * 55;
	HasGun = true;
	animationHelper.HoldType = CitizenAnimationHelper.HoldTypes.Pistol;
	var bulletGo = bullet.Clone(pos);
	var rb = bulletGo.Components.GetInAncestorsOrSelf<Rigidbody>();
	rb.Velocity = animationHelper.EyeWorldTransform.Rotation.Forward * 2000 + Vector3.Up * 64;

	
}
}
