using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Upgrade_Item_Weapon : UI_Base
{
    WeaponController WeaponController;

    enum GameObjects
    {
        ItemImage,
        ItemIcon,
        ItemName,
        ItemEXP
    }

    public override void Init()
    {
        // 게임 오브젝트 바인드
        Bind<GameObject>(typeof(GameObjects));
    }

    public void SetInfo(string abilityName, int level)
    {
        Get<GameObject>((int)GameObjects.ItemIcon).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Sprites/Items/weapon/" + abilityName);
        Get<GameObject>((int)GameObjects.ItemName).GetComponent<TMP_Text>().text = abilityName + " Lv." + level;
        Get<GameObject>((int)GameObjects.ItemEXP).GetComponentInChildren<TMP_Text>().text = "x " + level;
    }
}
