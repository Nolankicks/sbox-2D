using Sandbox;

public class Despawner : Component {
  public TimeUntil destroy = 1f;
  protected override void OnFixedUpdate(){
    if(destroy < 0f)GameObject.Destroy();
	
  }
}
