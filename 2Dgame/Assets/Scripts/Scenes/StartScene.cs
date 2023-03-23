using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : BaseScene
{
    // �α��� ��
    // ���� ����
    protected override void Init()
    {
        base.Init();

        // �� Ÿ�� ����
        SceneType = Define.Scene.Start;
        Managers.UI.ShowPopUpUI<UI_Start>();

        SetResolution();
    }

    // �� �ʱ�ȭ
    public override void Clear()
    {
        Debug.Log("Login Scene Clear!");
    }

    // �ػ� ����
    public void SetResolution()
    {
        int width = 1920;
        int height = 1080;

        Screen.SetResolution(width, height, false);
    }
}
