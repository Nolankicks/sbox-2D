using System.Security.Cryptography.X509Certificates;
using Sandbox;
using Sandbox.Citizen;

public sealed class TestPlayer : Component
{
	[Property] public Vector3 Gravity { get; set; } = new Vector3( 0, 0, 800 );

	[Property] public SkinnedModelRenderer skinnedModelRenderer { get; set; }
	private TimeSince timesinceInput;
	public Vector3 velocity;
	public Vector3 WishVelocity { get; private set; }
	[Property] public CitizenAnimationHelper AnimationHelper { get; set; }
	//[Property] public CitizenAnimationHelper.MoveStyles moveStyles {get; set;}
	[Property] public CameraComponent cameraComponent {get; set;}
	[Property] public GameObject eye {get; set;}
	private TimeSince timeSinceJump;

	public Vector3 Velocity;
	public float VSpeed = 0;
	public bool IsRunning { get; set; }

	protected override void OnAwake()
	{
		
		
	}
	protected override void OnUpdate()
	{

		//var cc = GameObject.Components.Get<CharacterController>();
		Velocity += Scene.PhysicsWorld.Gravity * Time.Delta * 0.5f;
		
		//mouse
		/*
		turretYaw -= Input.MouseDelta.x * 0.1f;
		turretPitch += Input.MouseDelta.y * 0.1f;
		turretPitch = turretPitch.Clamp( -30, 30 );
		cameraComponent.Transform.Rotation = Rotation.From( turretPitch, turretYaw, 0 );
		*/
		//movement
		Vector3 movement = 0;
		
	
		
		if ( Input.Down( "Left" ) )
		{
			movement += Transform.World.Left;
			skinnedModelRenderer.Transform.Rotation = Rotation.FromYaw( 90 );
			timesinceInput = 0;
			//AnimationHelper.MoveStyle = moveStyles;
			
			}

		if ( Input.Down( "Right" ) )
		{
			movement += Transform.World.Right;
			skinnedModelRenderer.Transform.Rotation = Rotation.FromYaw( -90 );
			timesinceInput = 0;
			
			}



		if (timesinceInput > 5f)
		{
			skinnedModelRenderer.Transform.Rotation = Rotation.FromYaw( 180 );
		}
		if ( Input.Down( "Left" ) && Input.Down( "Right" ) )
		{
			skinnedModelRenderer.Transform.Rotation = Rotation.FromYaw( 180 );
		}
		if (Input.Pressed( "Jump" ))
		{
			var go = GameObject;
			var jumpPos = GameObject.Transform.Position + Vector3.Up * 100f;
			Log.Info("Jump");
			go.Transform.Position = jumpPos;
			
			//cc.IsOnGround = false;
			//timeSinceJump = 0;
		}

				var rot = GameObject.Transform.Rotation;
		var pos = GameObject.Transform.Position + movement * Time.Delta * 300.0f;
		Transform.Local = new Transform( pos, rot, 1 );
		cameraComponent.Transform.Position = skinnedModelRenderer.Transform.Position + Vector3.Backward *500f + Vector3.Up * 70f;
	}


}

