using Sandbox;

public class Despawner : Component
{
	public TimeUntil destroy = 1f;

	[Property] public float despawnTime { get; set; } = 1;

	protected override void OnStart()
	{
		destroy = despawnTime;
	}
	protected override void OnFixedUpdate()
	{
		if ( destroy < 0f ) GameObject.Destroy();

	}
}
