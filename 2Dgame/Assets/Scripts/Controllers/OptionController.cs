using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionController : MonoBehaviour
{
    public AudioMixer _MasterMixer;
    public TMP_Dropdown dropdown;
    public Slider BGMVolume;
    public Slider SFXVolume;
    public Toggle fullScreenToggle;

    public static string DROPDOWN_KEY = "DROPDOWN_KEY";
    public static string BGMVOLUME_KEY = "BGMVOLUME_KEY";
    public static string SFXVOLUME_KEY = "SFXVOLUME_KEY";
    public static string FULLSCREEN_KEY = "FULLSCREEN_KEY";

    void Awake()
    {
        if (PlayerPrefs.HasKey(DROPDOWN_KEY) == false) dropdown.value = 2;
        else dropdown.value = PlayerPrefs.GetInt(DROPDOWN_KEY);

        if (PlayerPrefs.HasKey(BGMVOLUME_KEY) == false) BGMVolume.value = 0;
        else BGMVolume.value = PlayerPrefs.GetFloat(BGMVOLUME_KEY);

        if (PlayerPrefs.HasKey(SFXVOLUME_KEY) == false) SFXVolume.value = 0;
        else SFXVolume.value = PlayerPrefs.GetFloat(SFXVOLUME_KEY);

        if (PlayerPrefs.HasKey(FULLSCREEN_KEY) == false) fullScreenToggle.isOn = false;
        else fullScreenToggle.isOn = System.Convert.ToBoolean(PlayerPrefs.GetInt(FULLSCREEN_KEY));
    }

    public void SetBGMVolume()
    {
        _MasterMixer.SetFloat("BGM", BGMVolume.value);
        PlayerPrefs.SetFloat(BGMVOLUME_KEY, BGMVolume.value);
    }

    public void SetSFXVolume()
    {
        _MasterMixer.SetFloat("SFX", SFXVolume.value);
        PlayerPrefs.SetFloat(SFXVOLUME_KEY, SFXVolume.value);
    }

    int screenWidth = 1920;
    int screenHeight = 1080;
    bool isFullScreen = true;
    public void SetResolution()
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

        PlayerPrefs.SetInt(DROPDOWN_KEY, num);
        Screen.SetResolution(screenWidth, screenHeight, isFullScreen);
        Debug.Log(Screen.width + " + " + Screen.height + ", " + Screen.fullScreen);

        Debug.Log(num);
    }

    public void SetFullScreen()
    {
        isFullScreen = fullScreenToggle.isOn;
        Screen.SetResolution(screenWidth, screenHeight, isFullScreen);
        PlayerPrefs.SetInt(FULLSCREEN_KEY, System.Convert.ToInt32(isFullScreen));
        Debug.Log(Screen.width + " + " + Screen.height + ", " + Screen.fullScreen);
    }
}
