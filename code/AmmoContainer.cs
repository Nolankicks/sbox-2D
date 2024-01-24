using Sandbox;

public sealed class AmmoContainer : Component
{
	private int ammo = 0;
	[Property] public int Ammo
	{
		get => ammo;
		set
		{
			ammo = value;
		}
	}
	[Property] public int MaxAmmo { get; set; } = 30;

	[Property] public bool HasAmmo => Ammo > 0;
}
