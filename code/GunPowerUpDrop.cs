using System.Runtime.CompilerServices;
using Sandbox;

public sealed class GunPowerUpDrop : Component, Component.ITriggerListener //Change "Trigger to the name of your file
{

 [Property] public Attack attack {get; set;}
 [Property] public SoundEvent announcerVoice {get; set;}
 





    void ITriggerListener.OnTriggerEnter(Collider other)
    {
		if (other.Tags.Has("player"))
		{
			attack.ShowGun = true;
			attack.HasGun = true;
			GameObject.Destroy();
			Sound.Play(announcerVoice);
		}



	   

         
    }

    void ITriggerListener.OnTriggerExit( Collider other ) 
    {

		

	
    

    }

	


	
}
