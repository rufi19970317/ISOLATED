using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_EXP_Set : UI_Base
{
    PlayerStat _player;
    Define.EXPType _expType;

    enum GameObjects
    {
        EXPBar,
        EXPImage,
        EXPText,
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    public void SetInfo(Define.EXPType expType)
    {
        Get<GameObject>((int)GameObjects.EXPImage).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Sprites/Items/EXP/" + expType.ToString());
        switch(expType)
        {
            case Define.EXPType.WeaponEXP:
                break;
            case Define.EXPType.HouseEXP:
                Get<GameObject>((int)GameObjects.EXPImage).GetComponent<Image>().color = Color.magenta;
                break;
            case Define.EXPType.PlayerEXP:
                Get<GameObject>((int)GameObjects.EXPImage).GetComponent<Image>().color = Color.green;
                break;
        }

        Get<GameObject>((int)GameObjects.EXPText).GetComponent<TMP_Text>().text = "x0";
        _player = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
        _expType = expType;
    }

    public void OnUpdate(int expNum, float expRatio)
    {
        Get<GameObject>((int)GameObjects.EXPText).GetComponent<TMP_Text>().text = "x" + expNum.ToString();
        GetObject((int)GameObjects.EXPBar).GetComponent<Slider>().value = expRatio;
    }
}
