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
    }

    // �� �ʱ�ȭ
    public override void Clear()
    {
        Debug.Log("Login Scene Clear!");
    }
}
