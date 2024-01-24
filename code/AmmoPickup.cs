using Sandbox;

public sealed class AmmoPickup : Component, Component.ITriggerListener
{
	[Property] public SMG smg { get; set; }
	protected override void OnUpdate()
	{

	}
	void ITriggerListener.OnTriggerEnter(Collider other)
	{
		if (other.Tags.Has("player"))
		{
		smg.ammo = 180;
		smg.playerAnimation.Target.Set("b_reload", true);
		smg.ableShoot = true;
		}
	}
	void ITriggerListener.OnTriggerExit(Collider other)
	{

	}
}
