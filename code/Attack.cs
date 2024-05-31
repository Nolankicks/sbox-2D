using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using Sandbox;
using Sandbox.Citizen;

public sealed class Attack : Component
{
	public TimeSince timeSinceSpawn { get; set; }
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
		var body = Scene.Components.Get<SkinnedModelRenderer>( FindMode.EverythingInDescendants );
		if(Input.Pressed("attack1"))
	{	
			Fire();
			animationHelper.Target.Set("b_attack", true);
			
	}
	






	void Fire()
	{
		//Perform a trace foward
		animationHelper.HoldType = CitizenAnimationHelper.HoldTypes.Punch;
		var camFoward = body.Transform.Rotation;
		var tr = Scene.Trace.Ray(body.Transform.Position, body.Transform.Position + camFoward.Forward * 100).WithTag("bad").Run();

			if (Input.Pressed("attack1") && tr.Hit)
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
			if (Input.Pressed("attack1"))
			{
				Sound.Play(punchSound);
			}
			}
		}

}
}
