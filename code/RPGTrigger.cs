using Sandbox;

public sealed class RPGTrigger : Component, Component.ITriggerListener
{
	public bool destroyed = false;
	[Property] public GameObject explosion { get; set; }


	
	
	
	protected override void OnUpdate()
	{

	}
	public void OnTriggerEnter( Collider other )
	{
		var parent = Components.GetInParent<RPGCollision>();
		if (other.GameObject.Tags.Has("bad"))
		{
		other.GameObject.Destroy();
		

		
		}
		GameObject.Destroy();
	}
	public void OnTriggerExit( Collider other )
	{
		return;
	}
}
