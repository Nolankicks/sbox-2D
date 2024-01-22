using Sandbox;

public sealed class Spinner : Component
{
	[Property] public Rotation rotation { get; set; }
	protected override void OnUpdate()
	{
		GameObject.Transform.Rotation *= rotation;
	}
}
