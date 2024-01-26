using System.Linq;
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
		var manager = Scene.GetAllComponents<Manager>().FirstOrDefault();
		var parent = Components.GetInParent<RPGCollision>();
		if (other.GameObject.Tags.Has("bad"))
		{
		other.GameObject.Destroy();
		GameObject.Destroy();
		manager.ShouldAddScore = true;
		Log.Info("Trigger");
		
		}
		
	}
	public void OnTriggerExit( Collider other )
	{
			
		
		
	}
}
