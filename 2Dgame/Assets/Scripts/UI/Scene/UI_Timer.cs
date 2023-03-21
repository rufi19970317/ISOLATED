using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Timer : UI_Scene
{
    enum GameObjects
    {
        Timer
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
    }

    public void SetTimer(int minute, int second)
    {
        if(minute < 10 && second < 10)
            Get<GameObject>((int)GameObjects.Timer).GetComponent<TMP_Text>().text = $"0{minute}:0{second}";
        else if(minute < 10 && second >= 10)
            Get<GameObject>((int)GameObjects.Timer).GetComponent<TMP_Text>().text = $"0{minute}:{second}";
        else if(minute >= 10 && second < 10)
            Get<GameObject>((int)GameObjects.Timer).GetComponent<TMP_Text>().text = $"{minute}:0{second}";
        else
            Get<GameObject>((int)GameObjects.Timer).GetComponent<TMP_Text>().text = $"{minute}:{second}";
    }
}
