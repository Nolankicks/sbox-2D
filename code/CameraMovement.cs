using System;
using Sandbox;

public class CameraMovement : Component
{
	[Property] public Action ExampleAction { get; set; }
	CameraComponent cameraComponent => GameObject.Components.GetInParentOrSelf<CameraComponent>();

	protected override void OnUpdate()
	{


			ExampleAction?.Invoke();

	}
}
