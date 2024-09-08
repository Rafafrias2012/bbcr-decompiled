using UnityEngine;

public class InputTypeManager : MonoBehaviour
{
	private static InputTypeManager itm;

	public static bool usingTouch;

	private void Awake()
	{
        UnityEngine.Input.simulateMouseWithTouches = false;
		if (itm == null)
		{
			itm = this;
			Object.DontDestroyOnLoad(base.gameObject);
		}
		else if (itm != this)
		{
			Object.Destroy(base.gameObject);
		}
	}

	private void Update()
	{
		if (UnityEngine.Input.touchCount > 0 && !usingTouch)
		{
            usingTouch = true;
		}
		else if (UnityEngine.Input.anyKeyDown && usingTouch)
		{
            usingTouch = false;
		}
	}
}
