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
		if (body.Transform.Rotation != Rotation.FromYaw(90) && body.Transform.Rotation != Rotation.FromYaw(-90))
		{
			body.Transform.Rotation = Rotation.FromYaw(90);
		}
	


		IsSprinting = Input.Down("Run");
		var cam = Scene.GetAllComponents<CameraComponent>().FirstOrDefault();
		var campos = eye.Transform.Position;
		cam.Transform.Position = campos + Vector3.Backward * 5000f;

		if (GameObject.Transform.Position.x != 0 )
		{
			GameObject.Transform.Position = GameObject.Transform.Position.WithX(0);
		}
		if (cc.IsOnGround && Input.Pressed ("Jump"))
		{
			cc.Punch( Vector3.Up * 500 );
			animationHelper?.TriggerJump();
		Sound.Play( "ui.navigate.forward" );
	

	}

		UpdateAnimations();
	}
	protected override void OnFixedUpdate()
	{
		Move();
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
		var halfGrav = Scene.PhysicsWorld.Gravity * Time.Delta * 0.5f;
		var CC = cc;
		WishVelocity = Input.AnalogMove.Normal;

		if (!WishVelocity.IsNearlyZero())
		{
			WishVelocity = new Angles(0, 0, 0).ToRotation() * WishVelocity;
			WishVelocity = WishVelocity.WithZ(0);
			WishVelocity.ClampLength(1);
			WishVelocity *= RunSpeed();
			if (!cc.IsOnGround)
			{
				WishVelocity.ClampLength(50);
			}
		}

	CC.ApplyFriction( GetFriction() );
	if (CC.IsOnGround)
	{
		CC.Accelerate(WishVelocity);
		CC.Velocity = cc.Velocity.WithZ(0);
	}
	else
	{
		CC.Velocity += halfGrav;
		CC.Accelerate(WishVelocity);
	}
	CC.Move();
	if (!CC.IsOnGround)
	{
		cc.Velocity += halfGrav;
	}
	else
	{
		cc.Velocity = cc.Velocity.WithZ(0);
	}
	}
	

	public float GetFriction()
	{
		if (cc.IsOnGround)
		{
			return Friction;
		}
		else
		{
			return AirControl;
		}
	}

	public float RunSpeed()
	{
		if (Input.Down("run"))
		{
			return Sprint;
		}
		else
		{
			return Speed;
		}
	}
}
