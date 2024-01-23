using System.Linq;
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
	[Property] public bool Run = false;
	[Property] float gunRange {get; set;}
	[Property] bool GunEnabled {get; set;} = false;
	[Property] public SoundEvent gunSound {get; set;}

	
	protected override void OnUpdate()
	{
		
		if (ShowGun)
		{
			gun.Enabled = true;
		}
		if (!GunEnabled)
		{
			timeSinceSpawn = 0;
		}
		if (!ShowGun)
		{
			gun.Enabled = false;
		}

		

		var body = Scene.Components.Get<SkinnedModelRenderer>( FindMode.EverythingInDescendants );
		if(Input.Pressed("attack1"))
		{	
			Fire();
			
				
			GunPowerUp();
			animationHelper.Target.Set("b_attack", true);
			
	}
	






	void Fire()
	{
		//Perform a trace foward
		animationHelper.HoldType = CitizenAnimationHelper.HoldTypes.Punch;
		var camFoward = animationHelper.EyeWorldTransform.Position;
		var tr = Scene.Trace.Ray(camFoward, camFoward + (camFoward * Range)).WithAnyTags("bad").Run();

			if (Input.Pressed("attack1") && tr.Hit && !HasGun)
			{
			
			
		
			var roation = Rotation.FromYaw(90);
			var lookDir =  body.Transform.Rotation.Forward;
			var spawnRot = body.Transform.Rotation * 180;
			
			manager.AddScore();
			Log.Info("test");
			var trgo = tr.GameObject;
			var trgoPos = trgo.Transform.Position;
			var ragollPos = trgo.Transform.Position + lookDir * 100;
			Sound.Play(deathSound);
			Log.Info(trgo.Name);
			trgo.Destroy();
			//var ragdollGo = ragdoll.Clone(trgoPos, rotation);
			particleEffect.Clone(trgoPos);
			
			var ragdollClone = ragdoll.Clone(trgoPos + Vector3.Backward * 75, spawnRot);
			var ragdollRb = ragdollClone.Components.GetInAncestorsOrSelf<Rigidbody>();
			ragdollRb.Velocity = rotation.Forward * 1000;
			
			}
			if (Input.Pressed("attack1") && !HasGun)
			{
				Sound.Play(punchSound);
			}
			}
		}
	

void GunPowerUp()
{
	
	
	if (timeSinceSpawn > 10)
	{
		HasGun = false;
		ShowGun = false;
		GunEnabled = false;
	}
	if (Input.Pressed("attack1") && HasGun)
	{
		
	GunEnabled = true;
	var camFoward = animationHelper.EyeWorldTransform.Position;
	var pos = body.Transform.Position + Vector3.Up * 55;
	HasGun = true;
	animationHelper.HoldType = CitizenAnimationHelper.HoldTypes.Pistol;
	var bulletGo = bullet.Clone(pos);
	var rb = bulletGo.Components.GetInAncestorsOrSelf<Rigidbody>();
	rb.Velocity = animationHelper.EyeWorldTransform.Rotation.Forward * 2000 + Vector3.Up * 55;
	Sound.Play(gunSound);
	ShowGun = true;
	
	
	}
	

	
}
}
