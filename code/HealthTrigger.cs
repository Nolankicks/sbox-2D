using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Sandbox;

[Group( "Trigger" )]
public sealed class HealthTrigger : Component, Component.ITriggerListener //Change "Trigger to the name of your file
{

[Property] public HealthManager healthManager {get; set;}
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
		if (other.Tags.Has( "bullet" ))
		{
			Log.Info( "Triggered" );
			healthManager.healthNumber -= 25;
		}
		//Log.Info( "Triggered" );
        //managerref.EndGame();
		//healthManager.healthNumber = 0;
		
        
    }

    void ITriggerListener.OnTriggerExit( Collider other ) 
    {

		

	
    

    }

	

	


	
}
