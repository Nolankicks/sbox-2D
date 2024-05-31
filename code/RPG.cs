using System.Security.Authentication;
using Sandbox;
using Sandbox.Citizen;

public sealed class RPG : Component
{
	[Property] public Attack attack { get; set; }
	
	[Property] public GameObject rpgModel { get; set; }
	[Property] public GameObject shell { get; set; }
	[Property] public float speed { get; set; }
	[Property] public GameObject body { get; set; }
	[Property] public float Lifetime { get; set; }
	[Property] public CitizenAnimationHelper animationHelper { get; set; }
	protected override void OnUpdate()
	{
		/*if (attack.RPGGunEnabled)
		{
			animationHelper.HoldType = CitizenAnimationHelper.HoldTypes.RPG;
			rpgModel.Enabled = true;
		}

		if (Input.Pressed("attack1") && attack.RPGGunEnabled)
		{
			Shoot();
			
		}*/
	}

	public void Shoot()
	{
		var shellGo = shell.Clone(body.Transform.Position + Vector3.Up * 50f + Vector3.Forward * 50f, body.Transform.Rotation);
		var rb = shellGo.Components.Get<Rigidbody>();
		var collider = shellGo.Components.Get<BoxCollider>();
		rb.Velocity = body.Transform.Rotation.Forward * 1000;
		
		



	}
}
