using Sandbox;

public sealed class HurtTrigger : Component, Component.ITriggerListener
{
	protected override void OnUpdate()
	{

	}

	void ITriggerListener.OnTriggerEnter(Collider other)
	{
		if (other.GameObject.Parent.Components.TryGet<PlayerController>(out var player, FindMode.EverythingInSelfAndParent))
		{
			player.TakeDamage(10);
		}
	}
	void ITriggerListener.OnTriggerExit(Sandbox.Collider other)
	{

	}
}
