using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using Sandbox;
using Sandbox.Citizen;

public sealed class Attack : Component
{
	public TimeSince timeSinceSpawn { get; set; }
	[Property] float Range { get; set; }
	[Property] public SMG smg { get; set; }
	[Property] public GameObject particleEffect {get; set;}
	[Property] GameObject eye {get; set;}
	[Property] SkinnedModelRenderer body {get; set;}
	[Property] CitizenAnimationHelper animationHelper {get; set;}
	[Property] public SoundEvent deathSound {get; set;}
	[Property] SoundEvent soundEvent { get; set; }
	[Property] public GameObject ragdoll { get; set; }
	[Property] public Rotation rotation { get; set; }
	[Property] public Manager manager {get; set;}
	[Property] public SoundEvent punchSound {get; set;}
	[Property] public GameObject bullet {get; set;}
	[Property] public GameObject pistol {get; set;}
	[Property] public GameObject smgGun {get; set;}
	[Property] public bool HasGunPistol = false;
	[Property] public bool ShowGunPistol = false;
	[Property] public bool ShowGunSmg = false;
	[Property] public bool HasGunSmg = false;
	[Property] public bool Run = false;
	[Property] float gunRange {get; set;}
	[Property] bool PistolGunEnabled {get; set;} = false;
	[Property] public SoundEvent gunSound {get; set;}

	protected override void OnAwake()
	{
		HasGunSmg = true;
		ShowGunSmg = true;
		HasGunSmg = true;
	}
	protected override void OnUpdate()
	{
		if (ShowGunSmg)
		{
			smgGun.Enabled = true;
		}
		if (!ShowGunSmg)
		{
			smgGun.Enabled = false;
		}
		
		if (ShowGunPistol)
		{
			pistol.Enabled = true;
		}
		if (!PistolGunEnabled)
		{
			timeSinceSpawn = 0;
		}
		if (!ShowGunPistol)
		{
			pistol.Enabled = false;
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

			if (Input.Pressed("attack1") && tr.Hit && !HasGunPistol)
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
			if (Input.Pressed("attack1") && !HasGunPistol)
			{
				Sound.Play(punchSound);
			}
			}
		}
	

void GunPowerUp()
{
	
	
	if (timeSinceSpawn > 10)
	{
		HasGunPistol = false;
		ShowGunPistol = false;
		PistolGunEnabled = false;
	}
	if (Input.Pressed("attack1") && HasGunPistol)
	{
		
	PistolGunEnabled = true;
	var camFoward = animationHelper.EyeWorldTransform.Position;
	var pos = body.Transform.Position + Vector3.Up * 55;
	HasGunPistol = true;
	animationHelper.HoldType = CitizenAnimationHelper.HoldTypes.Pistol;
	var bulletGo = bullet.Clone(pos);
	var rb = bulletGo.Components.GetInAncestorsOrSelf<Rigidbody>();
	rb.Velocity = animationHelper.EyeWorldTransform.Rotation.Forward * 2000 + Vector3.Up * 55;
	Sound.Play(gunSound);
	
	ShowGunPistol = true;
	
	
	}
	

	
}
}
