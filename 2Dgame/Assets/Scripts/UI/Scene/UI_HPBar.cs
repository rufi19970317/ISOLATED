using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_Scene
{
    enum GameObjects
    {
        HPBar
    }

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
    }

    public void UpdateHP(float ratio)
    {
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
    }
}
