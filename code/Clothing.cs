using Sandbox;

public sealed class Clothing : Component
{
	[Property] public SkinnedModelRenderer body {get; set;}
	public void OnNetworkSpawn( Connection owner )
	{
		var clothing = new ClothingContainer();
		clothing.Deserialize( owner.GetUserData( "avatar" ) );
		clothing.Apply( body );
	}
}
