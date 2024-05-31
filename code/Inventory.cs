using Sandbox;

public sealed class Inventory : Component
{
	[Property] public GameObject CurrentObject { get; set; }
	[Property] public GameObject FistsObject { get; set; }
	protected override void OnStart()
	{
		var newFists = FistsObject.Clone();
		CurrentObject = newFists;
		FistsObject.Parent = GameObject;
	}

	public async void SetCurrentObjectAsync(GameObject obj, float time)
	{
		CurrentObject.Destroy();
		var newObj = obj.Clone();
		CurrentObject = newObj;
		obj.Parent = GameObject;
		await GameTask.DelaySeconds(time);
		CurrentObject.Destroy();
		var newFists = FistsObject.Clone();
		CurrentObject = newFists;
		FistsObject.Parent = GameObject;
	}
	public void SetCurrentObject(GameObject obj)
	{
		CurrentObject.Destroy();
		var newObj = obj.Clone();
		CurrentObject = newObj;
		obj.Parent = GameObject;
	}
	public void ResetWeapons()
	{
		CurrentObject.Destroy();
		var newFists = FistsObject.Clone();
		CurrentObject = newFists;
		FistsObject.Parent = GameObject;
	}
}
