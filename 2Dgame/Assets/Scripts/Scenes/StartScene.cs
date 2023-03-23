using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : BaseScene
{
    // 로그인 씬
    // 시작 세팅
    protected override void Init()
    {
        base.Init();

        // 씬 타입 설정
        SceneType = Define.Scene.Start;
        Managers.UI.ShowPopUpUI<UI_Start>();

        SetResolution();
    }

    // 씬 초기화
    public override void Clear()
    {
        Debug.Log("Login Scene Clear!");
    }

    // 해상도 고정
    public void SetResolution()
    {
        int width = 1920;
        int height = 1080;

        Screen.SetResolution(width, height, false);
    }
}
