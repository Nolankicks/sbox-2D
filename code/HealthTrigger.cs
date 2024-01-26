using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Sandbox;

[Group( "Trigger" )]
public sealed class HealthTrigger : Component, Component.ITriggerListener //Change "Trigger to the name of your file
{

[Property] public HealthManager healthManager {get; set;}
[Property] public SoundEvent hurtSound {get; set;}
[Property] public GameObject emitter {get; set;}
 bool _iTouching; 

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
		var playerPos = GameObject.Transform.Position;
		var emitterPose = playerPos + Vector3.Up * 64;
		if (!other.GameObject.IsValid)
		return;
		if (other.Tags.Has( "bullet" ))
		{
			Log.Info( "Triggered" );
			healthManager.healthNumber -= 15;
			Sound.Play(hurtSound);
			emitter.Clone(emitterPose);
			
		}
		//Log.Info( "Triggered" );
        //managerref.EndGame();
		//healthManager.healthNumber = 0;
		
        
    }

    void ITriggerListener.OnTriggerExit( Collider other ) 
    {

		

	
    

    }

	

	


	
}
