using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Sandbox;

[Group( "Trigger" )]
public sealed class ShooterTrigger : Component, Component.ITriggerListener //Change "Trigger to the name of your file
{

 bool _iTouching; 

[Property] public GameObject particleEffect {get; set;}

[Property] public GameObject humanRagdoll {get; set;}
[Property] public GameObject zombieRagdoll {get; set;}
Manager manager => Scene.GetAllComponents<Manager>().FirstOrDefault();
[Property] SkinnedModelRenderer body {get; set;}

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
		var pos = body.Transform.Position;	
		var rot = body.Transform.Rotation;	
		if (other.GameObject.Tags.Has("playerbullet"))
		{

			
			GameObject.Parent.Destroy();
			
			if (GameObject.Tags.Has("human"))
			{
			humanRagdoll.Clone(pos + Vector3.Backward * 75, rot);
			}
			if (GameObject.Tags.Has("zombie"))
			{
			zombieRagdoll.Clone(pos + Vector3.Backward * 75, rot);
			}

		manager.AddScore();
		particleEffect.Clone(pos);
		}		
			

		//Log.Info( "Triggered" );
        //managerref.EndGame();
		//healthManager.healthNumber = 0;
		
        
    }

	 public void OnTriggerExit( Collider other )
    {



	}
}


	

	


	

