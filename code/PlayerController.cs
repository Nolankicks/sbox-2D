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
	public CitizenAnimationHelper animationHelper;
	[Sync] public Vector3 WishVelocity { get; set; }
	[Property, Sync] public int Health { get; set; } = 100;
	[Property] public GameObject BloodEffect { get; set; }
	[Property] public SceneFile CurrentScene { get; set; }
	public bool IsSprinting;
	public bool IsRunning;
	[Property] public int MaxHealth { get; set; } = 200;
	public Manager manager;
	protected override void OnStart()
	{
		manager = Scene.GetAllComponents<Manager>().FirstOrDefault();
		animationHelper = Components.Get<CitizenAnimationHelper>(FindMode.EverythingInSelfAndChildren);
	}
	protected override void OnFixedUpdate()
	{
		UpdateAnimations();
		if (!IsProxy)
		{
		Move();
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
		if (body.Transform.Rotation != Rotation.FromYaw(90) && body.Transform.Rotation != Rotation.FromYaw(-90))
		{
			body.Transform.Rotation = Rotation.FromYaw(90);
		}
		if (Input.Pressed("right"))
		{
			body.Transform.Rotation = Rotation.FromYaw(-90);
		}
		if (Input.Pressed("left"))
		{
			body.Transform.Rotation = Rotation.FromYaw(90);
		}
	}
	
	}
	public void Heal(int amount)
	{
		Health += amount;
		if (Health > MaxHealth)
		{
			Health = MaxHealth;
		}
	}
	public void TakeDamage(int damage)
	{
		Health -= damage;
		BloodEffect.Clone(GameObject.Transform.Position + Vector3.Up * 55);
		if (Health <= 0)
		{
			Health = 0;
			Death();
		}
	}
	public void Death()
	{
		manager.EndGame();
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
		WishVelocity = Input.AnalogMove.Normal.WithX(0);

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
		CC.Velocity += halfGrav;
	}
	else
	{
		CC.Velocity = CC.Velocity.WithZ(0);
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
