using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;
[Group( "Trigger" )]
public sealed class HealthBoost : Component, Component.ITriggerListener //Change "Trigger to the name of your file
{

	bool _iTouching;

	[Property] public SoundEvent healthSound { get; set; }
	[Property] public GameObject crushedCan { get; set; }
	PlayerController PlayerController;


	protected override void OnStart()
	{
		PlayerController = Scene.GetAllComponents<PlayerController>().FirstOrDefault();

	}

	void ITriggerListener.OnTriggerEnter( Collider other )
	{
		if ( other.GameObject.Parent.Components.TryGet<PlayerController>( out var player, FindMode.EverythingInSelfAndParent ) )
		{
			player.Heal( 50 );
			Sound.Play( healthSound );
			GameObject.Destroy();
			crushedCan?.Clone( GameObject.Transform.Position );
		}
	}
}
