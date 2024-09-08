using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
	public Slider slider;

	public Toggle rumble;

	public Toggle analog;

  public Toggle fullScreenToggle;

    public TMP_Dropdown resolution;
    private void Start()
    {
		UpdateFullScreenToggleOnStart();
		for(int i = 0;i < Screen.resolutions.Length;i++)
		{
			resolution.options.Add(new TMP_Dropdown.OptionData(Screen.resolutions[i].width.ToString() + "x" + Screen.resolutions[i].height.ToString()));
			if(!PlayerPrefs.HasKey("OptionsSet") && Display.main.systemHeight== Screen.resolutions[i].height&& Display.main.systemWidth == Screen.resolutions[i].width)
			{
				resolution.value = i;
			}
		}
        if (PlayerPrefs.HasKey("OptionsSet"))
		{
			resolution.value = PlayerPrefs.GetInt("currentResolution");
			Screen.SetResolution(Screen.resolutions[resolution.value].width, Screen.resolutions[resolution.value].height,fullScreenToggle.isOn);
			slider.value = PlayerPrefs.GetFloat("MouseSensitivity");
			if (PlayerPrefs.GetInt("Rumble") == 1)
			{
				rumble.isOn = true;
			}
			else
			{
				rumble.isOn = false;
			}
			if (PlayerPrefs.GetInt("AnalogMove") == 1)
			{
				analog.isOn = true;
			}
			else
			{
				analog.isOn = false;
			}
		}
		else
		{
			PlayerPrefs.SetInt("OptionsSet", 1);
		}
	}
	public void UpdateResolution()
	{
		PlayerPrefs.SetInt("currentResolution", resolution.value);
        Screen.SetResolution(Screen.resolutions[resolution.value].width, Screen.resolutions[resolution.value].height, fullScreenToggle.isOn);
        PlayerPrefs.Save();
	}	
	private void Update()
	{
		PlayerPrefs.SetFloat("MouseSensitivity", slider.value);
		if (rumble.isOn)
		{
			PlayerPrefs.SetInt("Rumble", 1);
		}
		else
		{
			PlayerPrefs.SetInt("Rumble", 0);
		}
		if (analog.isOn)
		{
			PlayerPrefs.SetInt("AnalogMove", 1);
		}
		else
		{
			PlayerPrefs.SetInt("AnalogMove", 0);
		}
		PlayerPrefs.Save();
	}
    public void UpdateFullScreenToggle()
    {
		Screen.fullScreen = fullScreenToggle.isOn;
    }
    public void UpdateFullScreenToggleOnStart()
    {
      fullScreenToggle.isOn  =  Screen.fullScreen;
    }
}
