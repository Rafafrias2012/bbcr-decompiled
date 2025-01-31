using TMPro;
using UnityEngine;

public class DetentionTextScript : MonoBehaviour
{
	public DoorScript door;

	private TMP_Text text;

	private void Start()
	{
		text = GetComponent<TMP_Text>();
	}

	private void Update()
	{
		if (door.lockTime > 0f)
		{
			text.text = "You have detention! \n" + Mathf.CeilToInt(door.lockTime) + " seconds remain!";
		}
		else
		{
			text.text = string.Empty;
		}
	}
}
