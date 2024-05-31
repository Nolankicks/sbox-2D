using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using Sandbox;
using Sandbox.Citizen;

public sealed class Attack : Component
{
	public TimeSince timeSinceSpawn { get; set; }
	[Property] public GameObject particleEffect {get; set;}
	[Property] CitizenAnimationHelper animationHelper {get; set;}
	[Property] public SoundEvent deathSound {get; set;}
	[Property] public GameObject humanRagdoll { get; set; }
	[Property] public GameObject zombieRagdoll { get; set; }	
	[Property] public Rotation rotation { get; set; }
	public Manager manager {get; set;}
	[Property] public SoundEvent punchSound {get; set;}
	public PlayerController controller { get; set; }
	protected override void OnStart()
	{
		manager = Scene.GetAllComponents<Manager>().FirstOrDefault();
	}
	protected override void OnUpdate()
	{
	controller = Scene.GetAllComponents<PlayerController>().FirstOrDefault( x => !x.IsProxy);
	if (controller is null) return;
	animationHelper = controller.animationHelper;
	if(Input.Pressed("attack1") && animationHelper is not null)
	{	
			if (!IsProxy)
			{
				Fire();
			}
			animationHelper.Target.Set("b_attack", true);	
	}
}
void Fire()
	{
		animationHelper.HoldType = CitizenAnimationHelper.HoldTypes.Punch;
		var body = controller.body.GameObject;
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
