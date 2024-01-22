using Sandbox;

public sealed class CrushedCan : Component
{
	bool canSound = true;
	[Property] public SoundEvent crushedCan {get; set;}
	protected override void OnUpdate()
	{
		if (canSound)
		{
			PlaySound();
			
		}
	}
	void PlaySound()
	{
		Sound.Play(crushedCan);
		canSound = false;
	}
}
