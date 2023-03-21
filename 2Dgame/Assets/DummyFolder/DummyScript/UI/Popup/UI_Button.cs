using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Button : UI_Popup
{
    // 버튼 UI
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

    // 세팅
    public override void Init()
    {
        base.Init();

        // 모든 버튼 바인드
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        // 버튼 이벤트 바인드
        Get<Button>((int)Buttons.PointButton).gameObject.BindEvent(OnButtonClicked);

        // UI 오브젝트 가져오기
        GameObject go = Get<Image>((int)Images.ItemIcon).gameObject;
        // 드래그 이벤트 추가
        BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; } , Define.UIEvent.Drag);
    }

    // 클릭 이벤트
    int _score = 0;
    public void OnButtonClicked(PointerEventData data)
    {
        _score++;
        GetText((int)Texts.ScoreText).text = $"점수 : {_score}";
    }
}