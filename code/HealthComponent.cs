using Sandbox;

public sealed class HealthComponent : Component
{
	[Property] public float Health { get; set; } = 100;
	protected override void OnUpdate()
	{

	}
	public void TakeDamage(float damage)
	{
		Health -= damage;
		if (Health <= 0)
		{
			GameObject.Destroy();
		}
	}
}
