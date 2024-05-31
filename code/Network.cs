using System.Linq;
using Sandbox;
using Sandbox.Network;

public sealed class Network : Component, Component.INetworkListener
{
	[Property] public GameObject PlayerPrefab { get; set; }
	protected override void OnUpdate()
	{
		if (!GameNetworkSystem.IsActive)
		{
			GameNetworkSystem.CreateLobby();
		}
	}

	void INetworkListener.OnActive(Sandbox.Connection conn)
	{
		var spawns = Scene.GetAllComponents<SpawnPoint>().ToList();
		if (spawns.Count != 0)
		{
			var spawn = Game.Random.FromList(spawns);
			var player = PlayerPrefab.Clone(spawn.Transform.World);
			player.NetworkSpawn(conn);
		}
		else
		{
			var player = PlayerPrefab.Clone(GameObject.Transform.World);
			player.NetworkSpawn(conn);
		}
	}
}
