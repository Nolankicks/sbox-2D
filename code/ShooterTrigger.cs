using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Sandbox;

[Group( "Trigger" )]
public sealed class ShooterTrigger : Component, Component.ITriggerListener //Change "Trigger to the name of your file
{

 bool _iTouching; 
 [Property] public GameObject badguy {get; set;}
 [Property] public GameObject particleEffect {get; set;}
 [Property] public GameObject ragdoll {get; set;}
[Property] public Manager manager {get; set;}

 public void istouching(bool _iTouching)
 {
		if (_iTouching == true)
		{
			
		}
 }



	protected override void OnStart()
    {
      _iTouching = false;
		
    }

    public void OnTriggerEnter( Collider other )
    {
		if (!other.GameObject.IsValid)
		return;
		var pos = badguy.Transform.Position;	
		var rot = badguy.Transform.Rotation;	
		if (other.GameObject.Tags.Has("playerbullet"))
		{

			
			badguy.Destroy();
			ragdoll.Clone(pos + Vector3.Backward * 75, rot);
			manager.AddScore();
			particleEffect.Clone(pos);
			
		}		
			
		
		//Log.Info( "Triggered" );
        //managerref.EndGame();
		//healthManager.healthNumber = 0;
		
        
    }

    void ITriggerListener.OnTriggerExit( Collider other ) 
    {
		
		

	
    

    }
}
	

	


	

