using System;
using UnityEngine;

[Serializable]
public class MovementModifier
{
	public Vector3 movementAddend;

	public float movementMultiplier = 1f;

	public bool forceTrigger;

	public MovementModifier(Vector3 movementAddend, float movementMultiplier)
	{
		this.movementAddend = movementAddend;
		this.movementMultiplier = movementMultiplier;
	}
}
