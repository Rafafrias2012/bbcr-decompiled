using System.Collections.Generic;
using UnityEngine;

public class ActivityModifier : MonoBehaviour
{
	public List<MovementModifier> moveMods = new List<MovementModifier>();

	public float Multiplier
	{
		get
		{
			float num = 1f;
			foreach (MovementModifier moveMod in moveMods)
			{
				num *= moveMod.movementMultiplier;
			}
			return num;
		}
	}

	public Vector3 Addend
	{
		get
		{
			Vector3 zero = Vector3.zero;
			foreach (MovementModifier moveMod in moveMods)
			{
				zero += moveMod.movementAddend * Time.deltaTime;
			}
			return zero;
		}
	}

	public bool ForceTrigger
	{
		get
		{
			for (int i = 0; i < moveMods.Count; i++)
			{
				if (moveMods[i].forceTrigger)
				{
					return true;
				}
			}
			return false;
		}
	}
}
