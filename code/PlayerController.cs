using System;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using Sandbox;
using Sandbox.Citizen;

public sealed class PlayerController : Component
{
	[Property] public Vector3 Gravity { get; set; } = new Vector3( 0, 0, 800 );
	[Property] public float Speed { get; set; } = 100;
	
	[Property] public float Friction { get; set; } = 6;
	[Property] public float JumpForce { get; set; } = 900;
	[Property] public GameObject eye {get; set;}
	CitizenAnimationHelper animationHelper;
	public Vector3 Velocity;
	private TimeSince timeSinceJump;
	Vector3 movement;
	protected override void OnStart()
	{
		animationHelper = Components.Get<CitizenAnimationHelper>(FindMode.EverythingInSelfAndChildren);
	}
	protected override void OnUpdate()
	{
		
		var cam = Scene.GetAllComponents<CameraComponent>().FirstOrDefault();
		var campos = eye.Transform.Position;
		cam.Transform.Position = campos + Vector3.Backward *500f;


		
		var cc = Components.Get<CharacterController>(FindMode.EnabledInSelf);
			if (Input.Down("Left"))
			{
				movement += Vector3.Left * Friction * Speed * Time.Delta;
			}
			if (Input.Down("Right"))
			{
				movement += Vector3.Right * Friction * Speed * Time.Delta;
			}
			Transform.Position = Transform.Position.WithY(Math.Clamp(movement.y, -150, 150));

		if ( animationHelper is not null )
		{
			animationHelper.IsGrounded = true;
			animationHelper.WithWishVelocity( -Velocity );
			animationHelper.WithVelocity( -Velocity );
			animationHelper.Height = 1f;
			animationHelper.IsGrounded = cc.IsOnGround;
		}
		if ( cc.IsOnGround )
		{
			cc.Velocity = cc.Velocity.WithZ( 0 );
			cc.Accelerate( Velocity );
			cc.ApplyFriction( 4.0f );
		}
		else
		{
			cc.Velocity -= Gravity * Time.Delta * 0.5f;
			cc.Accelerate( Velocity.ClampLength( 50 ) );
			cc.ApplyFriction( 0.1f );
		}

		cc.Move();

		if ( !cc.IsOnGround )
		{
			cc.Velocity -= Gravity * Time.Delta * 0.5f;
		}
		else
		{
			cc.Velocity = cc.Velocity.WithZ( 0 );
		}
		if (Input.Pressed ("Jump"))
		{
			cc.Punch( Vector3.Up * 300 );
			animationHelper?.TriggerJump();
		Sound.Play( "ui.navigate.forward" );
			

	}

	}
	protected override void OnFixedUpdate()
	{
		
		//Velocity += Scene.PhysicsWorld.Gravity * Time.Delta * 0f;

		
		//Velocity += Scene.PhysicsWorld.Gravity * Time.Delta * 0.5f;

		
	void Jump()
	{

		
		animationHelper?.TriggerJump();
		Sound.Play( "ui.navigate.forward" );
		
	}
}
}
