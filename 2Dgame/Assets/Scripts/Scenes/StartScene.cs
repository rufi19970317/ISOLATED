using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : BaseScene
{
    public AudioMixer _MasterMixer;

    // 로그인 씬
    // 시작 세팅
    protected override void Init()
    {
        base.Init();

        // 씬 타입 설정
        SceneType = Define.Scene.Start;
        Managers.UI.ShowPopUpUI<UI_Start>();

        SetResolution();
        SetAudio();
    }

    // 씬 초기화
    public override void Clear()
    {
        Debug.Log("Login Scene Clear!");
    }

    // 해상도 고정
    public void SetResolution()
    {
        int screenWidth = 1920;
        int screenHeight = 1080;

        int num = 0;

        if (PlayerPrefs.HasKey(OptionController.DROPDOWN_KEY) == false) num = 2;
        else num = PlayerPrefs.GetInt(OptionController.DROPDOWN_KEY);

        bool isfullscreen = false;
        if (PlayerPrefs.HasKey(OptionController.FULLSCREEN_KEY) == false) isfullscreen = false;
        else isfullscreen = System.Convert.ToBoolean(PlayerPrefs.GetInt(OptionController.FULLSCREEN_KEY));

        switch (num)
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

        Screen.SetResolution(screenWidth, screenHeight, isfullscreen);
    }

    public void SetAudio()
    {
        float BGMvalue = 0;
        float SFXvalue = 0;

        if (PlayerPrefs.HasKey(OptionController.BGMVOLUME_KEY) == false) BGMvalue = 0;
        else BGMvalue = PlayerPrefs.GetFloat(OptionController.BGMVOLUME_KEY);

        if (PlayerPrefs.HasKey(OptionController.SFXVOLUME_KEY) == false) SFXvalue = 0;
        else SFXvalue = PlayerPrefs.GetFloat(OptionController.SFXVOLUME_KEY);
    }
}
