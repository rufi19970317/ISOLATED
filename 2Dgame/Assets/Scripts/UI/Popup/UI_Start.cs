using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Start : UI_Popup
{
    enum GameObjects
    {
        GameStart,
        Option,
        Exit
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        Get<GameObject>((int)GameObjects.GameStart).BindEvent((PointerEventData) =>
        {
            Managers.Scene.LoadScene(Define.Scene.Game);
        });

        Get<GameObject>((int)GameObjects.Option).BindEvent((PointerEventData) =>
        {
            transform.Find("OptionPannel").gameObject.SetActive(true);
        });

        Get<GameObject>((int)GameObjects.Exit).BindEvent((PointerEventData) =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit(); // 어플리케이션 종료
#endif
        });
    }
}
