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
	[Property] public GameObject humanRagdoll { get; set; }
	[Property] public GameObject zombieRagdoll { get; set; }	
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
	[Property] public bool DuccBlasterEnabled {get; set;} = false;
	[Property] public SoundEvent gunSound {get; set;}
	[Property] public GameObject impactEffect {get; set;}
	[Property] public RPG rpg {get; set;}
	[Property] public DuccBlaster duccBlaster {get; set;}
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
			RPGGunEnabled = false;
			PistolGunEnabled = false;
		}
		if (!SmgGunEnabled)
		{
			smgGun.Enabled = false;
		}
		
		if (PistolGunEnabled)
		{
			pistol.Enabled = true;
			RPGGunEnabled = false;
			SmgGunEnabled = false;
		}
		if (!PistolGunEnabled && !RPGGunEnabled && !DuccBlasterEnabled)
		{
			timeSinceSpawn = 0;
		}
		if (!PistolGunEnabled)
		{
			pistol.Enabled = false;
		}
		
		if (timeSinceSpawn > 10)
		{
			RPGGunEnabled = false;
			DuccBlasterEnabled = false;
		}

		if (!RPGGunEnabled)
		{
			rpg.rpgModel.Enabled = false;
		}
		if (RPGGunEnabled)
		{
			PistolGunEnabled = false;
			SmgGunEnabled = false;
		}
		if (DuccBlasterEnabled)
		{
			PistolGunEnabled = false;
			SmgGunEnabled = false;
			RPGGunEnabled = false;
			duccBlaster.blaster.Enabled = true;
			
		}
		if (!DuccBlasterEnabled)
		{
			duccBlaster.blaster.Enabled = false;
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
		var camFoward = body.Transform.Rotation;
		var tr = Scene.Trace.Ray(body.Transform.Position, body.Transform.Position + camFoward.Forward * 100).WithTag("bad").Run();

			if (Input.Pressed("attack1") && tr.Hit && !PistolGunEnabled && !SmgGunEnabled && !RPGGunEnabled && !DuccBlasterEnabled)
			{
			
			
			
			var roation = Rotation.FromYaw(90);
			var lookDir =  body.Transform.Rotation.Forward;
			var spawnRot = body.Transform.Rotation * -1;
			
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
			
			if (tr.GameObject.Tags.Has("human"))
			{
			var huragdollClone = humanRagdoll.Clone(trgoPos + Vector3.Backward * 75, spawnRot);
			}
			else
			{
			var zoragdollClone = zombieRagdoll.Clone(trgoPos + Vector3.Backward * 75, spawnRot);
			}
			if (Input.Pressed("attack1") && !SmgGunEnabled && !PistolGunEnabled && !RPGGunEnabled)
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
}
