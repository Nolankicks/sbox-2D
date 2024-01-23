using Sandbox;

public sealed class PlayMusic : Component
{
	[Property] public GameObject gameObject;
	protected override void OnUpdate()
	{
		if (Input.Pressed("map"))
		{
			gameObject.Enabled = !gameObject.Enabled;
		}
	}
}
