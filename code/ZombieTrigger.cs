using Sandbox;

public sealed class ZombieTrigger : Component, Component.ITriggerListener
{
	[Property] public HealthManager healthManager { get; set; }
	[Property] public SoundEvent hurtSound { get; set; }
	protected override void OnUpdate()
	{

	}

	void ITriggerListener.OnTriggerEnter(Collider other)
	{
		if (other.Tags.Has("player"))
		{
		healthManager.healthNumber -= 25;
		Sound.Play(hurtSound);
		}
	}
	
	void ITriggerListener.OnTriggerExit(Collider other)
	{
		
	}
}
