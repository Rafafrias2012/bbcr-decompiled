using UnityEngine;
using UnityEngine.UI;

public class ClassicLoadScreen : MonoBehaviour
{
	public RectTransform faceAnchor;

	private Vector3 pos;

	private float val;

	private float height;

	private float speed;

	private float time;
	private void OnEnable()
	{
		UpdatePosition();
	}

	private void Update()
	{
			UpdatePosition();
	}

	private void UpdatePosition()
	{
		val += 48f * Time.unscaledDeltaTime * speed;
		time += Time.unscaledDeltaTime * speed;
		speed = Mathf.Min(1f, speed + Time.unscaledDeltaTime);
		if (val >= 192f)
		{
			val -= 192f;
		}
		height = Mathf.Sin(time) * 128f;
		pos.x = Mathf.RoundToInt(val);
		pos.y = Mathf.RoundToInt(height);
		faceAnchor.localPosition = pos;
	}
}
