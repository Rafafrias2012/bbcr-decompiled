using System.Collections;
using UnityEngine;

public class ExitButtonScript : MonoBehaviour
{
	public AudioSource audioSource;
	public void ExitGame()
	{
		base.StartCoroutine(Exit());
	}
	public IEnumerator Exit()
	{
		Cursor.visible = false;
		Cursor.lockState =CursorLockMode.Locked;
		audioSource.Play();
		float t = audioSource.clip.length;
		while (t > 0)
		{
			t -= Time.unscaledDeltaTime;
			yield return null;
		}
		Application.Quit();
	}
}
