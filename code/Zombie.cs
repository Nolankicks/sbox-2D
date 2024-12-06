using System.Linq;
using Sandbox;
using Sandbox.Citizen;

public sealed class Zombie : Component
{
	CitizenAnimationHelper citizenAnimationHelper => Components.Get<CitizenAnimationHelper>( FindMode.InSelf );
	[Property] public SkinnedModelRenderer body { get; set; }
	[Property] public CharacterController characterController { get; set; }
	PlayerController controller => Scene.GetAllComponents<PlayerController>().FirstOrDefault();
	[Property] public float Speed { get; set; }
	public Vector3 target;
	public Vector3 WishVelocity;
	[Property] public SoundEvent traceHitSound { get; set; }
	public TimeSince timeSinceHit = 0;
	protected override void OnStart()
	{
		citizenAnimationHelper.HoldType = CitizenAnimationHelper.HoldTypes.Swing;
	}
	protected override void OnUpdate()
	{
		if ( !controller.IsValid() || !body.IsValid() || !characterController.IsValid() )
			return;

		target = controller.GameObject.Transform.Position;
		BuildWishVelocity();
		UpdateMovement();
		UpdateAnimations();
		Trace();

	}

	void BuildWishVelocity()
	{
		WishVelocity = (target - Transform.Position).Normal;

		WishVelocity = WishVelocity.Normal;
		WishVelocity = WishVelocity * Speed;
	}

	void UpdateMovement()
	{
		characterController.ApplyFriction( GetFriction() );

		if ( characterController.IsOnGround )
		{
			characterController.Accelerate( WishVelocity );
			characterController.Velocity = characterController.Velocity.WithZ( 0 );
		}
		
		characterController.Move();
	}
	float GetFriction()
	{
		if ( characterController.IsOnGround ) return 6.0f;

		return 0.2f;
	}
	void UpdateAnimations()
	{
		if ( target != Vector3.Zero && body.IsValid() )
		{
			var targetRot = Rotation.LookAt( target.WithZ( Transform.Position.z ) - Transform.Position, Vector3.Up );
			body.Transform.Rotation = Rotation.Slerp( body.Transform.Rotation, targetRot, Time.Delta * 10f );
		}

		citizenAnimationHelper?.WithWishVelocity( WishVelocity );
		citizenAnimationHelper?.WithVelocity( characterController.Velocity );
	}

	void Trace()
	{
		if ( !body.IsValid() )
			return;

		var lookDir = body.Transform.Rotation;
		var tr = Scene.Trace.Ray( body.Transform.Position, body.Transform.Position + lookDir.Forward * 50 ).WithoutTags( "bad" ).Run();
		if ( tr.Hit && tr.GameObject.Tags.Has( "player" ) )
		{
			if ( timeSinceHit > 2 )
			{
				if ( tr.GameObject.Parent.Components.TryGet<PlayerController>( out var player, FindMode.EverythingInSelfAndAncestors ) )
				{
					player.TakeDamage( 10 );
				}

				timeSinceHit = 0;

				Sound.Play( traceHitSound, tr.HitPosition );

				citizenAnimationHelper?.Target?.Set( "b_attack", true );
			}

		}
	}
}
