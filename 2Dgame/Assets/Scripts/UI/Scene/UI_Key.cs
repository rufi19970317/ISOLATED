using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Key : UI_Scene
{
    enum GameObjects
    {
        KeyPanel
    }

    int keyNum = 0;

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
    }

    public void UpdateKey()
    {
        GetObject((int)GameObjects.KeyPanel).transform.GetChild(keyNum++).GetComponent<Image>().color = new Color(1, 1, 1, 1);
    }

    public int GetKeyNum()
    {
        return keyNum;
    }
}
