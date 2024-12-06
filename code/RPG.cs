using System.Linq;
using System.Security.Authentication;
using Sandbox;
using Sandbox.Citizen;

public sealed class RPG : Component
{
	[Property] public GameObject shell { get; set; }
	public GameObject body { get; set; }
	public CitizenAnimationHelper animationHelper { get; set; }
	protected override void OnUpdate()
	{
		body = Scene.GetAllComponents<PlayerController>()?.FirstOrDefault()?.body?.GameObject;
		animationHelper = Scene.GetAllComponents<PlayerController>()?.FirstOrDefault()?.animationHelper;

		if ( animationHelper.IsValid() )
			animationHelper.HoldType = CitizenAnimationHelper.HoldTypes.RPG;
			
		if ( Input.Pressed( "attack1" ) )
		{
			Shoot();
		}
	}

	public void Shoot()
	{
		if ( !body.IsValid() )
			return;

		var shellGo = shell.Clone( body.Transform.Position + Vector3.Up * 50f + Vector3.Forward * 50f, body.Transform.Rotation );
		var rb = shellGo.Components.Get<Rigidbody>();

		if ( rb.IsValid() )
			rb.Velocity = body.Transform.Rotation.Forward * 1000;
	}
}
