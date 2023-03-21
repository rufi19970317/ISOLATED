using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Button : UI_Popup
{
    // ��ư UI
    enum Buttons
    {
        PointButton
    }

    enum Texts
    {
        PointText,
        ScoreText
    }

    enum GameObjects
    {
        TestObject,
    }

    enum Images
    {
        ItemIcon,
    }

    private void Start()
    {
        Init();
    }

    // ����
    public override void Init()
    {
        base.Init();

        // ��� ��ư ���ε�
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        // ��ư �̺�Ʈ ���ε�
        Get<Button>((int)Buttons.PointButton).gameObject.BindEvent(OnButtonClicked);

        // UI ������Ʈ ��������
        GameObject go = Get<Image>((int)Images.ItemIcon).gameObject;
        // �巡�� �̺�Ʈ �߰�
        BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; } , Define.UIEvent.Drag);
    }

    // Ŭ�� �̺�Ʈ
    int _score = 0;
    public void OnButtonClicked(PointerEventData data)
    {
        _score++;
        GetText((int)Texts.ScoreText).text = $"���� : {_score}";
    }
}