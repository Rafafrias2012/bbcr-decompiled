using UnityEngine;
using UnityEngine.SceneManagement;

public class WarningScreenScript : MonoBehaviour
{
	private void Update()
	{
		if (UnityEngine.Input.anyKeyDown)
		{
			SceneManager.LoadScene("MainMenu");
		}
	}
}
