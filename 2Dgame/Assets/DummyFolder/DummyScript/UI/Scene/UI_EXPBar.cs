using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EXPBar : UI_Scene
{
    /*
    enum GameObjects
    {
        EXPBar
    }

    PlayerStat _stat;
    GameObject _camera;
    int TotalExp;

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));
    }

    private void Update()
    {
        TotalExp = _stat.TotalExp;
        float ratio = _stat.Exp / (float)TotalExp;
        SetEXPRatio(ratio);
    }

    public void SetEXPRatio(float ratio)
    {
        GetObject((int)GameObjects.EXPBar).GetComponent<Slider>().value = ratio;
    }

    public void SetPlayer()
    {
        _stat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
    }
    */
}
