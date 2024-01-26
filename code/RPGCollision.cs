using Sandbox;

public sealed class RPGCollision : Component, Component.ICollisionListener
{
	
	[Property] public SoundEvent explosionSound { get; set; }
	[Property] public GameObject explosion { get; set; }
	[Property] public GameObject trigger { get; set; }
	[Property] public GameObject self { get; set; }
	int explosionCount = 0;
	int collisionCount = 0;
	protected override void OnUpdate()
	{
		
	}

	public void OnCollisionStart( Collision o )
	{
		
		Log.Info("Collision");
		explosion.Clone(GameObject.Transform.Position, GameObject.Transform.Rotation);
		explosionCount += 1;
		
		collisionCount += 1;
		if (explosionCount == 1)
		{
			explosion.Clone(GameObject.Transform.Position, GameObject.Transform.Rotation);
			trigger.Clone(GameObject.Transform.Position, GameObject.Transform.Rotation);
		}
		if (collisionCount > 0)
		{
			GameObject.Destroy();
		}
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
