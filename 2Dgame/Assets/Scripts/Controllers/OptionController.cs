using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    public AudioMixer _MasterMixer;

    public void SetMasterVolume(Slider volume)
    {
        _MasterMixer.SetFloat("Master", volume.value);
    }

    public void SetBGMVolume(Slider volume)
    {
        _MasterMixer.SetFloat("BGM", volume.value);
    }

    public void SetSFXVolume(Slider volume)
    {
        _MasterMixer.SetFloat("SFX", volume.value);
    }

    int screenWidth = 1920;
    int screenHeight = 1080;
    bool isFullScreen = true;
    public void SetResolution(TMP_Dropdown dropdown)
    {
        int num = dropdown.value;
        switch(num)
        {
            case 0:
                screenWidth = 3840;
                screenHeight = 2160;
                break;
            case 1:
                screenWidth = 2560;
                screenHeight = 1440;
                break;
            case 2:
                screenWidth = 1920;
                screenHeight = 1080;
                break;
            case 3:
                screenWidth = 1280;
                screenHeight = 720;
                break;
            case 4:
                screenWidth = 1600;
                screenHeight = 900;
                break;
            case 5:
                screenWidth = 800;
                screenHeight = 600;
                break;
        }
        Screen.SetResolution(screenWidth, screenHeight, isFullScreen);
        Debug.Log(Screen.width + " + " + Screen.height + ", " + Screen.fullScreen);

        Debug.Log(num);
    }

    public void SetFullScreen(Toggle toggle)
    {
        isFullScreen = toggle.isOn;
        Screen.SetResolution(screenWidth, screenHeight, isFullScreen);
        Debug.Log(Screen.width + " + " + Screen.height + ", " + Screen.fullScreen);
    }
}
