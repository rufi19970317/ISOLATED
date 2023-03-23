using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Util;

public class UI_Upgrade_Item : UI_Base
{
    PlayerStat stat;
    WeaponController WeaponController;
    HouseController HouseController;
    PlayerController PlayerController;

    enum GameObjects
    {
        ItemBackground1,
        ItemBackground2,
        ItemIcon,
        ItemUpgrade
    }

    public override void Init()
    {
        // 게임 오브젝트 바인드
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.ItemBackground2).SetActive(false);
    }

    public void SetInfo(Define.Ability ability, string abilityName)
    {
        stat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
        switch (ability)
        {
            case Define.Ability.Weapon:
                WeaponController = Managers.Game.GetPlayer().GetComponent<WeaponController>();
                Define.Weapon weaponAbility = EnumUtil<Define.Weapon>.Parse(abilityName);
                Get<GameObject>((int)GameObjects.ItemIcon).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Sprites/Items/Weapon/" + abilityName);
                Get<GameObject>((int)GameObjects.ItemUpgrade).BindEvent((PointerEventData) =>
                {
                    if (stat.WeaponUpgradeNum >= 1)
                    {
                        int level = WeaponController.WeaponLevelUp(weaponAbility);
                        stat.WeaponUpgradeNum--;
                        if (level >= 8) UpgradeComplete();
                    }
                });
                break;
            case Define.Ability.House:
                Define.HouseAbility houseAbility = EnumUtil<Define.HouseAbility>.Parse(abilityName);
                
                break;
            case Define.Ability.Player:
                PlayerController = Managers.Game.GetPlayer().GetComponent<PlayerController>();
                Define.PlayerAbility playerAbility = EnumUtil<Define.PlayerAbility>.Parse(abilityName);
                Get<GameObject>((int)GameObjects.ItemUpgrade).BindEvent((PointerEventData) =>
                {
                    if (stat.PlayerUpgradeNum >= 1)
                    {
                        PlayerController.SetAbility(playerAbility);
                        UpgradeComplete();
                        stat.PlayerUpgradeNum--;
                    }
                });
                break;
        }
    }

    void UpgradeComplete()
    {
        Get<GameObject>((int)GameObjects.ItemBackground1).SetActive(false);
        Get<GameObject>((int)GameObjects.ItemBackground2).SetActive(true);
        Get<GameObject>((int)GameObjects.ItemUpgrade).SetActive(false);
    }
}