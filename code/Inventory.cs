using Sandbox;

public sealed class Inventory : Component
{
	[Property] public GameObject CurrentObject { get; set; }
	[Property] public GameObject FistsObject { get; set; }
	[Property] public SkinnedModelRenderer HoldObjectRenderer { get; set; }
	public static Inventory Instance { get; set; }
	protected override void OnStart()
	{
		Instance = this;
		HoldObjectRenderer.Enabled = false;
		if (IsProxy) return;
		var newFists = FistsObject.Clone();
		CurrentObject = newFists;
		newFists.Parent = GameObject;
	}

	public async void SetCurrentObjectAsync(GameObject obj, Model GunModel, float time, Vector3 Offset = default)
	{
		HoldObjectRenderer.Enabled = true;
		HoldObjectRenderer.Transform.LocalPosition = Offset;
		HoldObjectRenderer.Model = GunModel;
		CurrentObject.Destroy();
		var newObj = obj.Clone();
		CurrentObject = newObj;
		newObj.Parent = GameObject;
		await GameTask.DelaySeconds(time);
		if (obj == null) return;
		HoldObjectRenderer.Transform.LocalPosition = Vector3.Zero;
		HoldObjectRenderer.Enabled = false;
		CurrentObject.Destroy();
		var newFists = FistsObject.Clone();
		CurrentObject = newFists;
		FistsObject.Parent = GameObject;
	}
	public void SetCurrentObject(GameObject obj, Model GunModel, Vector3 Offset = default)
	{
		if (IsProxy) return;
		HoldObjectRenderer.Enabled = true;
		HoldObjectRenderer.Transform.LocalPosition = Offset;
		HoldObjectRenderer.Model = GunModel;
		CurrentObject.Destroy();
		var newObj = obj.Clone();
		newObj.NetworkSpawn();
		CurrentObject = newObj;
		newObj.Parent = GameObject;
	}
	public void ResetWeapons()
	{
		if (IsProxy) return;
		HoldObjectRenderer.Transform.LocalPosition = Vector3.Zero;
		HoldObjectRenderer.Enabled = false;
		CurrentObject.Destroy();
		var newFists = FistsObject.Clone();
		CurrentObject = newFists;
		newFists.Parent = GameObject;
	}
}
