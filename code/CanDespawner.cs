using Sandbox;

public class CanDespawner : Component {
	
  public TimeUntil destroy = 3f;
  protected override void OnFixedUpdate(){
    if(destroy < 0f)GameObject.Destroy();
	
  }
}
