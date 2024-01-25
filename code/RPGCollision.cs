using Sandbox;

public sealed class RPGCollision : Component, Component.ICollisionListener
{
	[Property] public GameObject explosion { get; set; }
	protected override void OnUpdate()
	{
		
	}

	public void OnCollisionStart( Collision o )
	{
		Log.Info("Collision");
		explosion.Clone(GameObject.Transform.Position, GameObject.Transform.Rotation);
		GameObject.Destroy();
	}


	public void OnCollisionStop( CollisionStop o )
	{
		return;
	}

	public void OnCollisionUpdate( Collision o )
	{
		return;
	}
}
