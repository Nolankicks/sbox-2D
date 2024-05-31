using System.Linq;
using Sandbox;

public sealed class DuccPowerUp : Component, Component.ITriggerListener
{
	Attack attack => Scene.GetAllComponents<Attack>().FirstOrDefault();
	protected override void OnUpdate()
	{

	}

	void ITriggerListener.OnTriggerEnter(Sandbox.Collider other)
	{
		if (other.Tags.Has("player"))
		{
			//attack.DuccBlasterEnabled = true;
			GameObject.Destroy();
		}
	}
	void ITriggerListener.OnTriggerExit(Sandbox.Collider other)
	{

	}
}
