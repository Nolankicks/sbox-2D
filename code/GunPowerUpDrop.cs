using System.Linq;
using System.Runtime.CompilerServices;
using Sandbox;

public sealed class GunPowerUpDrop : Component, Component.ITriggerListener //Change "Trigger to the name of your file
{

 [Property] public SoundEvent announcerVoice {get; set;}
[Property] public SoundEvent equipSound {get; set;}
[Property] public Attack attack {get; set;}




    void ITriggerListener.OnTriggerEnter(Collider other)
    {
		if (!other.GameObject.IsValid)
		return;
		if (other.Tags.Has("player"))
		{
			attack.ShowGun = true;
			attack.HasGun = true;
			GameObject.Destroy();
			Sound.Play(announcerVoice);
			Sound.Play(equipSound);
		}



	   

         
    }

    void ITriggerListener.OnTriggerExit( Collider other ) 
    {

		

	
    

    }

	


	
}
