using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameOver : UI_Popup
{
    enum Texts
    {
        GameOver,
        PressKey,
    }

    enum Buttons
    {
        Home,
    }
    void OnEnable()
    {
        Time.timeScale = 0f;
    }

    public override void Init()
    {
        base.Init();
        Bind<TMP_Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Get<Button>((int)Buttons.Home).gameObject.BindEvent(OnButtonClicked);
    }

    // Update is called once per frame
    public override void ClosePopupUI()
    {
        base.ClosePopupUI();
        Time.timeScale = 1f;
        Managers.Scene.LoadScene(Define.Scene.Start);
    }

    void OnButtonClicked(PointerEventData data)
    {
        ClosePopupUI();
    }
}
