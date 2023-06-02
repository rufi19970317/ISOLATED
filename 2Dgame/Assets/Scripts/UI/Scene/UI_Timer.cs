using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Timer : UI_Scene
{
    enum GameObjects
    {
        Timer,
        MonsterCount
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
    }

    public void SetObject(bool isDefense)
    {
        if(isDefense)
        {
            GetObject((int)GameObjects.Timer).SetActive(false);
            GetObject((int)GameObjects.MonsterCount).SetActive(true);
        }
        else
        {
            GetObject((int)GameObjects.Timer).SetActive(true);
            GetObject((int)GameObjects.MonsterCount).SetActive(false);
        }
    }

    public void SetTimer(float ratio)
    {
        GetObject((int)GameObjects.Timer).GetComponent<Slider>().value = ratio;
    }

    public void SetCount(string count)
    {
        GetObject((int)GameObjects.MonsterCount).GetComponent<TMP_Text>().text = count;
    }
}
