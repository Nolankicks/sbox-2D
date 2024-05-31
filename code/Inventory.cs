using Sandbox;

public sealed class Inventory : Component
{
	[Property] public GameObject CurrentObject { get; set; }
	[Property] public GameObject FistsObject { get; set; }
	[Property] public SkinnedModelRenderer HoldObjectRenderer { get; set; }
	protected override void OnStart()
	{
		HoldObjectRenderer.Enabled = false;
		if (IsProxy) return;
		var newFists = FistsObject.Clone();
		CurrentObject = newFists;
		newFists.Parent = GameObject;
	}

	public async void SetCurrentObjectAsync(GameObject obj, Model GunModel, float time)
	{
		HoldObjectRenderer.Enabled = true;
		HoldObjectRenderer.Model = GunModel;
		CurrentObject.Destroy();
		var newObj = obj.Clone();
		CurrentObject = newObj;
		obj.Parent = GameObject;
		await GameTask.DelaySeconds(time);
		HoldObjectRenderer.Enabled = false;
		CurrentObject.Destroy();
		var newFists = FistsObject.Clone();
		CurrentObject = newFists;
		FistsObject.Parent = GameObject;
	}
	public void SetCurrentObject(GameObject obj, Model GunModel)
	{
		if (IsProxy) return;
		HoldObjectRenderer.Enabled = true;
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
		HoldObjectRenderer.Enabled = false;
		CurrentObject.Destroy();
		var newFists = FistsObject.Clone();
		CurrentObject = newFists;
		FistsObject.Parent = GameObject;
	}
}
