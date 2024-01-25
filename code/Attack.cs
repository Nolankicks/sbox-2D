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
	[Property] public bool PistolGunEnabled {get; set;} = false;
	[Property] public bool SmgGunEnabled {get; set;} = false;
	[Property] public bool RPGGunEnabled {get; set;} = false;
	[Property] public SoundEvent gunSound {get; set;}
	[Property] public GameObject impactEffect {get; set;}
	[Property] public RPG rpg {get; set;}
	protected override void OnAwake()
	{
		/*
		HasGunSmg = true;
		ShowGunSmg = true;
		HasGunSmg = true;
		*/
	}
	protected override void OnUpdate()
	{
		if (SmgGunEnabled)
		{
			smgGun.Enabled = true;
		}
		if (!SmgGunEnabled)
		{
			smgGun.Enabled = false;
		}
		
		if (PistolGunEnabled)
		{
			pistol.Enabled = true;
		}
		if (!PistolGunEnabled)
		{
			timeSinceSpawn = 0;
		}
		if (!PistolGunEnabled)
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

			if (Input.Pressed("attack1") && tr.Hit && !PistolGunEnabled && !SmgGunEnabled)
			{
			
			
			
			var roation = Rotation.FromYaw(90);
			var lookDir =  body.Transform.Rotation.Forward;
			var spawnRot = body.Transform.Rotation * 180;
			
			manager.AddScore();
			Log.Info("test");
			var trgo = tr.GameObject;
			var trgoPos = trgo.Transform.Position;
			var ragollPos = trgo.Transform.Position + lookDir * 100;
			
			Sound.Play(deathSound, tr.HitPosition);
			Log.Info(trgo.Name);
			trgo.Destroy();
			//var ragdollGo = ragdoll.Clone(trgoPos, rotation);
			particleEffect.Clone(trgoPos);
			
			var ragdollClone = ragdoll.Clone(trgoPos + Vector3.Backward * 75, spawnRot);
			var ragdollRb = ragdollClone.Components.GetInAncestorsOrSelf<Rigidbody>();
			ragdollRb.Velocity = rotation.Forward * 1000;
			
			}
			if (Input.Pressed("attack1") && !SmgGunEnabled && !PistolGunEnabled)
			{
				Sound.Play(punchSound);
			}
			}
		}
	

void GunPowerUp()
{

		
		

	if (timeSinceSpawn > 10)
	{
		PistolGunEnabled = false;
	}
	if (Input.Pressed("attack1") && PistolGunEnabled)
	{
			
	
	PistolGunEnabled = true;
	var camFoward = body.Transform.Rotation;
	animationHelper.HoldType = CitizenAnimationHelper.HoldTypes.Pistol;
	Sound.Play(gunSound);

	var tr = Scene.Trace.Ray(body.Transform.Position, body.Transform.Position + camFoward.Forward * 5000).WithoutTags("player").Run();
	if (!tr.Hit) return;
	

	if (tr.Hit)
	{
		Log.Info("hit");
		var trgo = tr.GameObject;
		Log.Info(trgo);
		if (trgo.Tags.Has("bad"))
		{
			
			trgo.Destroy();
			Sound.Play(deathSound, tr.HitPosition);
			var deathParticle = particleEffect;
			deathParticle.Clone(new Transform(tr.HitPosition + Vector3.Up * 45, Rotation.LookAt(tr.Normal)));
			manager.AddScore();
			
		}

	}
	if (impactEffect is not null)
	{
		impactEffect.Clone(new Transform(tr.HitPosition +Vector3.Up * 45, Rotation.LookAt(tr.Normal)));
	}
	
	
	
	}
	

	
}
}
