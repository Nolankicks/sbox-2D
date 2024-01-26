using System;
using System.Security.Cryptography;
using Sandbox;

public sealed class GunItemSpawner : Component
{
    
    float PistolRandomNumber() => Random.Shared.Float(0, 100);
	 float RPGRandomNumber() => Random.Shared.Float(0, 100);
    [Property] public GameObject pistol {get; set;}
	[Property] public GameObject rpg {get; set;}
	[Property] public Attack attack {get; set;}
    protected override void OnFixedUpdate()
    {
		PistolRandomNumber();
		RPGRandomNumber();
		var rpgRandom = RPGRandomNumber();
		var pisolRandom = PistolRandomNumber();
        
        
		if (pisolRandom > 99.9f && !attack.SmgGunEnabled && !attack.PistolGunEnabled && !attack.RPGGunEnabled)
		{
			var pistolClone = pistol.Clone();
			
			pistolClone.Transform.Position = new Vector3(0, Random.Shared.Float(150, -500), 0);
		}
		if (rpgRandom > 99.9f && !attack.SmgGunEnabled && !attack.PistolGunEnabled && !attack.RPGGunEnabled)
		{
			var rpgClone = rpg.Clone();
			
			rpgClone.Transform.Position = new Vector3(0, Random.Shared.Float(150, -500), 0);
		}
    }
    }

