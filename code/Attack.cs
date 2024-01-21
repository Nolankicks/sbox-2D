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
	[Property] CitizenAnimationHelper.HoldTypes holdTypes {get; set;}
	[Property] SoundEvent soundEvent { get; set; }
	[Property] GameObject ragdoll { get; set; }
	[Property] float Damage { get; set; } = 10.0f;
	[Property] public Rotation rotation { get; set; }
	[Property] public Manager manager {get; set;}
	[Property] public SoundEvent punchSound {get; set;}
	protected override void OnUpdate()
	{
		var body = Scene.Components.Get<SkinnedModelRenderer>( FindMode.EverythingInDescendants );
		if(Input.Pressed("attack1"))
		{
			Fire();
			
			animationHelper.Target.Set("b_attack", true);
			Sound.Play(punchSound);
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
			
			Log.Info(trgo.Name);
			Sound.Play(soundEvent);
			trgo.Destroy();
			var ragdollGo = ragdoll.Clone(trgoPos, rotation);
			particleEffect.Clone(trgoPos);
			
			
		}
	}
}
}
