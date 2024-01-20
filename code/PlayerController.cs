using System;
using System.Linq;
using System.Runtime;
using System.Runtime.Intrinsics.X86;
using Sandbox;
using Sandbox.Citizen;

public sealed class PlayerController : Component
{
	[Property] public float Sprint {get; set;} = 3;
	[Property] public float AirControl {get; set;} = 0.1f;
	[Property] public float Speed { get; set; } = 100;
	[Property] public float MaxForce {get; set;} = 50f;
	[Property] public float Friction { get; set; } = 6;
	[Property] public float JumpForce { get; set; } = 900;
	[Property] public GameObject eye {get; set;}
	[Property] public SkinnedModelRenderer body {get; set;}
	[Property] public CharacterController cc {get; set;}
	//[Property] TimeSpan timeSpan {get; set;}
	CitizenAnimationHelper animationHelper;
	public Vector3 WishVelocity = Vector3.Zero;
	private TimeSince inputTime;
	Vector3 movement;
	public bool IsSprinting;
	public bool IsRunning;
	protected override void OnStart()
	{
		animationHelper = Components.Get<CitizenAnimationHelper>(FindMode.EverythingInSelfAndChildren);
	}
	protected override void OnUpdate()
	{
		IsSprinting = Input.Down("Run");
		var cam = Scene.GetAllComponents<CameraComponent>().FirstOrDefault();
		var campos = eye.Transform.Position;
		cam.Transform.Position = campos + Vector3.Backward * 5000f;

		if (GameObject.Transform.Position.x != 0 )
		{
			GameObject.Transform.Position = GameObject.Transform.Position.WithX(0);
		}
		



			/*if (Input.Down("Left"))
			{
				body.Transform.Rotation = Rotation.FromYaw(90);
				movement += Vector3.Left * Friction * Speed * Time.Delta;
				inputTime = 0;
			}
			if (Input.Down("Right"))
			{
				body.Transform.Rotation = Rotation.FromYaw(-90);
				movement += Vector3.Right * Friction * Speed * Time.Delta;
				inputTime = 0;
				
			}
			Transform.Position = Transform.Position.WithY(Math.Clamp(movement.y, -150, 150));
			*/
		
		
		if (cc.IsOnGround && Input.Pressed ("Jump"))
		{
			cc.Punch( Vector3.Up * 500 );
			animationHelper?.TriggerJump();
		Sound.Play( "ui.navigate.forward" );
	

	}
	if (inputTime > 0.1f)
	{
		body.Transform.Rotation = Rotation.FromYaw(180);
	}
		UpdateAnimations();
	}
	protected override void OnFixedUpdate()
	{
		BuildWishVelocity();
		Move();
	}
	void BuildWishVelocity()
	{
		WishVelocity = 0;
		if (Input.Down("Left"))
		{

			WishVelocity += Vector3.Left * Friction * Speed * Time.Delta;
			inputTime = 0;
			
			
			body.Transform.Rotation = Rotation.FromYaw(90);

		}
		if (Input.Down("Right"))
		{
			WishVelocity += Vector3.Right * Friction * Speed * Time.Delta;
			inputTime = 0;
			body.Transform.Rotation = Rotation.FromYaw(-90);
		}

		
		WishVelocity = WishVelocity.WithZ(0);
		if(!WishVelocity.IsNearZeroLength) WishVelocity = WishVelocity.Normal;
		WishVelocity *= Speed;

	}

	private void UpdateAnimations()
	{
		if ( animationHelper is not null )
		{
			animationHelper.IsGrounded = true;
			animationHelper.WithWishVelocity( WishVelocity );
			animationHelper.WithVelocity( WishVelocity );
			animationHelper.Height = 1f;
			animationHelper.IsGrounded = cc.IsOnGround;
			animationHelper.MoveStyle = CitizenAnimationHelper.MoveStyles.Run;
			


		}
	}
	void Move()
	{
		var gravity = Scene.PhysicsWorld.Gravity;

		if (cc.IsOnGround)
		{
			cc.Velocity = cc.Velocity.WithZ(0);
			cc.Accelerate(WishVelocity);
			cc.ApplyFriction(Friction);
		}

		else
		{
			cc.Velocity += gravity * Time.Delta * 0.5f;
			cc.Accelerate(WishVelocity.ClampLength(MaxForce));
			cc.ApplyFriction(AirControl);
		}
		cc.Move();

		if(!cc.IsOnGround)
		{
			cc.Velocity += gravity * Time.Delta * 0.5f;
		}
		else
		{
			cc.Velocity = cc.Velocity.WithZ(0);
		}
	}
	
}
