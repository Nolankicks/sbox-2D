using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sandbox;

public sealed class Spawner : Component
{
	[Property] public List<GameObject> ObjectsToSpawn { get; set; } = new();
	[Property] public bool RandomSpawnRate { get; set; }
	[Property, ShowIf("RandomSpawnRate", false)] public float SpawnRate { get; set; } = 5;
	[Property, ShowIf("RandomSpawnRate", true)] public int MinSpawnRate { get; set; } = 1;
	[Property, ShowIf("RandomSpawnRate", true)] public int MaxSpawnRate { get; set; } = 5;
	protected override void OnStart() 
	{
		_ = SpawnItems();
	}

	public async Task SpawnItems()
{
    while (true)
    {
        var spawns = Scene.GetAllComponents<SpawnPoint>().ToList();
        if (spawns.Count == 0 || ObjectsToSpawn.Count == 0) continue;

        var randomSpawn = Game.Random.FromList(spawns);
        var randomObject = Game.Random.FromList(ObjectsToSpawn);

        if (randomSpawn != null && randomObject != null)
        {
            var clonedObject = randomObject.Clone(randomSpawn.Transform.World);
            if (clonedObject == null)
            {
                Log.Error("Failed to clone object");
                continue;
            }
        }

        if (RandomSpawnRate)
        {
            await GameTask.DelayRealtimeSeconds(Random.Shared.Int(MinSpawnRate, MaxSpawnRate));
        }
        else
        {
            await GameTask.DelayRealtimeSeconds(SpawnRate);
        }
    }
}
}
