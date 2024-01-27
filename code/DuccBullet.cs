using Sandbox;

public sealed class DuccBullet : Component, Component.ICollisionListener
{
	protected override void OnUpdate()
	{

	}

	void ICollisionListener.OnCollisionStart(Sandbox.Collision other)
	{
		GameObject.Destroy();
	}
	void ICollisionListener.OnCollisionStop(Sandbox.CollisionStop other)
	{

	}
	void ICollisionListener.OnCollisionUpdate(Sandbox.Collision other)
	{

	}
}
