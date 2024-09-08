using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeightedSelection<T>
{
	public T selection;

	public int weight;

	public static T ControlledRandomSelection(WeightedSelection<T>[] items, System.Random rng)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		foreach (WeightedSelection<T> weightedSelection in items)
		{
			num3 += weightedSelection.weight;
		}
		int num4 = rng.Next(0, num3);
		for (num = 0; num < items.Length; num++)
		{
			num2 += items[num].weight;
			if (num2 > num4)
			{
				break;
			}
		}
		if (num < items.Length)
		{
			return items[num].selection;
		}
		Debug.Log("No valid selection found. Returning index 0");
		return items[0].selection;
	}

	public static T RandomSelection(WeightedSelection<T>[] items)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		foreach (WeightedSelection<T> weightedSelection in items)
		{
			num3 += weightedSelection.weight;
		}
		int num4 = UnityEngine.Random.Range(0, num3);
		for (num = 0; num < items.Length; num++)
		{
			num2 += items[num].weight;
			if (num2 > num4)
			{
				break;
			}
		}
		if (num < items.Length)
		{
			return items[num].selection;
		}
		Debug.Log("No valid selection found. Returning index 0");
		return items[0].selection;
	}

	public static T ControlledRandomSelectionList(List<WeightedSelection<T>> items, System.Random rng)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		foreach (WeightedSelection<T> item in items)
		{
			num3 += item.weight;
		}
		int num4 = rng.Next(0, num3);
		for (num = 0; num < items.Count; num++)
		{
			num2 += items[num].weight;
			if (num2 > num4)
			{
				break;
			}
		}
		if (num < items.Count)
		{
			return items[num].selection;
		}
		Debug.Log("No valid selection found. Returning index 0");
		return items[0].selection;
	}
}
