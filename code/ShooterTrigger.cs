using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Sandbox;

[Group( "Trigger" )]
public sealed class ShooterTrigger : Component, Component.ITriggerListener //Change "Trigger to the name of your file
{

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
		if (!other.GameObject.IsValid)
		return;

		if (other.GameObject.Tags.Has("playerbullet"))
		{
			GameObject.Destroy();
		}		
			
		
		//Log.Info( "Triggered" );
        //managerref.EndGame();
		//healthManager.healthNumber = 0;
		
        
    }

    void ITriggerListener.OnTriggerExit( Collider other ) 
    {
		
		

	
    

    }
}
	

	


	

