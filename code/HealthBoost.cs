using System.Runtime.CompilerServices;
using Sandbox;
[Group( "Trigger" )]
public sealed class HealthBoost : Component, Component.ITriggerListener //Change "Trigger to the name of your file
{

 bool _iTouching; 

[Property] public SoundEvent healthSound {get; set;}
[Property] public GameObject crushedCan {get; set;}
	


	protected override void OnStart()
    {
      _iTouching = false;

    }

    void ITriggerListener.OnTriggerEnter(Collider other)
    {
		var healthManager = other.GameObject.Components.Get<HealthManager>();
		if (other.Tags.Has("player"))
	{
		if (healthManager.healthNumber + 50f < healthManager.maxHealth)
		{
		healthManager.healthNumber += 50;
		}
		else
		{
			healthManager.healthNumber = healthManager.maxHealth;
		}
		
		Sound.Play(healthSound);
		GameObject.Destroy();
    	crushedCan.Clone(GameObject.Transform.Position);
	}
		

	   

         
    }

    void ITriggerListener.OnTriggerExit( Collider other ) 
    {

		
	Log.Info("Out");
	
    

    }

	


	
}
