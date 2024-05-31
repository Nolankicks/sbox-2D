using System.Linq;
using Sandbox;

public sealed class RPGPowerUpTrigger : Component, Component.ITriggerListener
{
	
	[Property] public SoundEvent powerUpSound { get; set; }
	protected override void OnUpdate()
	{

	}

	public void OnTriggerEnter( Collider other )
	{
		var attack = Scene.GetAllComponents<Attack>().FirstOrDefault();
		if (other.GameObject.Tags.Has("player"))
		{
			//attack.RPGGunEnabled = true;
			GameObject.Destroy();
			Sound.Play(powerUpSound);
		}
	}

	public void OnTriggerExit( Collider other )
	{
		
	}
}
