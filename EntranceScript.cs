using UnityEngine;

public class EntranceScript : MonoBehaviour
{
	public GameControllerScript gc;

	public Material map;

	public MeshRenderer wall;

	public void Lower()
	{
		base.transform.position =  new Vector3(base.transform.position.x, -10f, base.transform.position.z);
		if (gc.finaleMode)
		{
			wall.material = map;
		}
	}

	public void Raise()
	{
		base.transform.position =new Vector3(base.transform.position.x,0, base.transform.position.z);
	} 
}
