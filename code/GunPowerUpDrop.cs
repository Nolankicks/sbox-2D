using System.Runtime.CompilerServices;
using Sandbox;

public sealed class GunPowerUpDrop : Component, Component.ITriggerListener //Change "Trigger to the name of your file
{

 [Property] public Attack attack {get; set;}
 





    void ITriggerListener.OnTriggerEnter(Collider other)
    {
		if (other.Tags.Has("player"))
		{
			attack.ShowGun = true;
			GameObject.Destroy();
		}



	   

         
    }

    void ITriggerListener.OnTriggerExit( Collider other ) 
    {

		

	
    

    }

	


	
}
