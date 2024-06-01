using Sandbox;

public sealed class AddGunTrigger : Component, Component.ITriggerListener
{
	[Property] public bool Async { get; set; }
	[Property, ShowIf("Async", true)] public float Time { get; set; }
	public delegate void AddWeaponAction(Inventory inventory, Model model, GameObject Player);
	[Property] public Vector3 Offset { get; set; }
	[Property] public AddWeaponAction OnAddWeapon { get; set; }
	[Property] public Model GunModel { get; set; }
	[Property] public GameObject Gun { get; set; }
	protected override void OnUpdate()
	{

	}

	void ITriggerListener.OnTriggerEnter(Sandbox.Collider other)
	{
		if (other.GameObject.Parent.Components.TryGet<Inventory>(out var inv, FindMode.EverythingInSelfAndAncestors))
		{
			OnAddWeapon?.Invoke(inv, GunModel, other.GameObject.Parent);
			if (Async)
			{
				inv.SetCurrentObjectAsync(Gun, GunModel, Time, Offset);
			}
			else
			{
				inv.SetCurrentObject(Gun, GunModel, Offset);
			}
			GameObject.Destroy();
		}
	}

	void ITriggerListener.OnTriggerExit(Sandbox.Collider other)
	{

	}
}
