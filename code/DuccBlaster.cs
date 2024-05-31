using System.Linq;
using Sandbox;
using Sandbox.Citizen;

public sealed class DuccBlaster : Component
{
	[Property] public GameObject bullet { get; set; }
	public GameObject body { get; set; }
	public CitizenAnimationHelper animationHelper { get; set; }
	[Property] public SoundEvent quackSound { get; set; }
	protected override void OnUpdate()
	{
		body = Scene.GetAllComponents<PlayerController>().FirstOrDefault().body.GameObject;
		animationHelper = Scene.GetAllComponents<PlayerController>().FirstOrDefault().animationHelper;
		if (body is null || animationHelper is null) return;
		animationHelper.HoldType = CitizenAnimationHelper.HoldTypes.Pistol;
		
		if (Input.Pressed("attack1"))
		{
			Sound.Play(quackSound);
			animationHelper.Target.Set("b_attack", true);
			var bulletGo = bullet.Clone(body.Transform.Position + body.Transform.Rotation.Up * 45f + body.Transform.Rotation.Forward * 100f, body.Transform.Rotation);
			var rb = bulletGo.Components.Get<Rigidbody>();
			rb.Velocity = body.Transform.Rotation.Forward * 1000;
		}

		}

}
