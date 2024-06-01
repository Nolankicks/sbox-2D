using Sandbox;

public sealed class Spinner : Component
{
	protected override void OnFixedUpdate()
	{
		GameObject.Transform.Rotation *= new Angles(0, 1, 0).ToRotation();
	}
}
