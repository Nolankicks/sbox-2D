using System;
using System.Collections.Generic;
using Sandbox;

public sealed class PlatformSpawner : Component
{
	[Property] public float SpawnAhead { get; set; } = 100;
	[Property] GameObject PlatformPrefab { get; set; }
	[Property] List<GameObject> OtherPrefabs { get; set; }
	void SpawnPlatform(float height)
	{
		var platform = PlatformPrefab.Clone();
			platform.Transform.Position = new Vector3( 0, Random.Shared.Float( -140f, 140f ), height );
		platform.Transform.Scale = platform.Transform.Scale.WithY( Random.Shared.Float( 0.7f, 1.1f ) );
			if ( Random.Shared.Float() < 0.3f )
		{
			var others = new List<GameObject>();
			others.Add( PlatformPrefab );
			others.AddRange( OtherPrefabs );
			var prefab = others[Random.Shared.Next( 0, others.Count )];
			var obj = prefab.Clone();
			obj.Transform.Position = new Vector3( 0, Random.Shared.Float( -140f, 140f ), height + Random.Shared.Float( -60f, 60f ) );
	}
}
}
