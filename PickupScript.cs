using UnityEngine;

public class PickupScript : MonoBehaviour
{
	public GameControllerScript gc;

	public Transform player;

	private void Start()
	{
	}

	private void Update()
	{
		if (!Input.GetKeyDown(KeyCode.Mouse0) || Time.timeScale == 0f)
		{
			return;
		}
		Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo) && hitInfo.transform == transform)
		{
			if ((hitInfo.transform.name == "Pickup_EnergyFlavoredZestyBar") & (Vector3.Distance(player.position, base.transform.position) < 10f))
			{
				hitInfo.transform.gameObject.SetActive(false);
				gc.CollectItem(1);
			}
			else if ((hitInfo.transform.name == "Pickup_YellowDoorLock") & (Vector3.Distance(player.position, base.transform.position) < 10f))
			{
				hitInfo.transform.gameObject.SetActive(false);
				gc.CollectItem(2);
			}
			else if ((hitInfo.transform.name == "Pickup_Key") & (Vector3.Distance(player.position, base.transform.position) < 10f))
			{
				hitInfo.transform.gameObject.SetActive(false);
				gc.CollectItem(3);
			}
			else if ((hitInfo.transform.name == "Pickup_BSODA") & (Vector3.Distance(player.position, base.transform.position) < 10f))
			{
				hitInfo.transform.gameObject.SetActive(false);
				gc.CollectItem(4);
			}
			else if ((hitInfo.transform.name == "Pickup_Quarter") & (Vector3.Distance(player.position, base.transform.position) < 10f))
			{
				hitInfo.transform.gameObject.SetActive(false);
				gc.CollectItem(5);
			}
			else if ((hitInfo.transform.name == "Pickup_Tape") & (Vector3.Distance(player.position, base.transform.position) < 10f))
			{
				hitInfo.transform.gameObject.SetActive(false);
				gc.CollectItem(6);
			}
			else if ((hitInfo.transform.name == "Pickup_AlarmClock") & (Vector3.Distance(player.position, base.transform.position) < 10f))
			{
				hitInfo.transform.gameObject.SetActive(false);
				gc.CollectItem(7);
			}
			else if ((hitInfo.transform.name == "Pickup_WD-3D") & (Vector3.Distance(player.position, base.transform.position) < 10f))
			{
				hitInfo.transform.gameObject.SetActive(false);
				gc.CollectItem(8);
			}
			else if ((hitInfo.transform.name == "Pickup_SafetyScissors") & (Vector3.Distance(player.position, base.transform.position) < 10f))
			{
				hitInfo.transform.gameObject.SetActive(false);
				gc.CollectItem(9);
			}
			else if ((hitInfo.transform.name == "Pickup_BigBoots") & (Vector3.Distance(player.position, base.transform.position) < 10f))
			{
				hitInfo.transform.gameObject.SetActive(false);
				gc.CollectItem(10);
			}
			else if ((hitInfo.transform.name == "Pickup_Dirty Chalk Eraser") & (Vector3.Distance(player.position, base.transform.position) < 10f))
			{
				hitInfo.transform.gameObject.SetActive(false);
				gc.CollectItem(11);
			}
			else if ((hitInfo.transform.name == "Pickup_Dangerous Teleporter") & (Vector3.Distance(player.position, base.transform.position) < 10f))
			{
				hitInfo.transform.gameObject.SetActive(false);
				gc.CollectItem(12);
			}
			else if ((hitInfo.transform.name == "Pickup_Grappling Hook") & (Vector3.Distance(player.position, base.transform.position) < 10f))
			{
				hitInfo.transform.gameObject.SetActive(false);
				gc.CollectItem(13);
			}
			else if ((hitInfo.transform.name == "Pickup_Faculty Nametag") & (Vector3.Distance(player.position, base.transform.position) < 10f))
			{
				hitInfo.transform.gameObject.SetActive(false);
				gc.CollectItem(14);
			}
			else if ((hitInfo.transform.name == "Pickup_Principal Whistle") & (Vector3.Distance(player.position, base.transform.position) < 10f))
			{
				hitInfo.transform.gameObject.SetActive(false);
				gc.CollectItem(15);
			}
			else if ((hitInfo.transform.name == "Pickup_An Apple For Baldi") & (Vector3.Distance(player.position, base.transform.position) < 10f))
			{
				hitInfo.transform.gameObject.SetActive(false);
				gc.CollectItem(16);
			}
		}
	}
}
