using System;
using System.Security.Cryptography;
using Sandbox;

public sealed class GunItemSpawner : Component
{
    
    float GetRandomNumber() => Random.Shared.Float(0, 100);
    [Property] public GameObject gun {get; set;}
    protected override void OnFixedUpdate()
    {
		GetRandomNumber();
		var random = GetRandomNumber();
        
        
		if (random > 99.9f)
		{
			var gunClone = gun.Clone();
			
			gunClone.Transform.Position = new Vector3(0, Random.Shared.Float(-150, 150), 0);
		}
    }
    }

