using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Data;
using static Util;

public class UI_LevelUP : UI_Popup
{
    /*
    PlayerStat _playerStat;
    WeaponController _weaponController;
    PassiveController _passiveController;
    Dictionary<Define.Weapon, string> weaponToString = new Dictionary<Define.Weapon, string>();
    Dictionary<Define.Passive, string> passiveToString = new Dictionary<Define.Passive, string>();

    // UI
    enum GameObjects
    {
        ItemPanel1,
        ItemPanel2,
        ItemPanel3,
    }

    // 세팅
    public override void Init()
    {
        base.Init();

        _playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
        _weaponController = _playerStat.transform.GetComponent<WeaponController>();
        _passiveController = _playerStat.transform.GetComponent<PassiveController>();

        // 모든 버튼 바인드
        Bind<GameObject>(typeof(GameObjects));

        // 레벨업 할 수 있는 아이템 넣기
        List<string> items = new List<string>();
        foreach (WeaponStat weaponStat in Managers.Data.WeaponLv[0].Values)
        {
            if(_weaponController.weaponToString.ContainsValue(weaponStat.name))
            {
                if (_weaponController.weaponsStat[EnumUtil<Define.Weapon>.Parse(weaponStat.name)].level < 8)
                {
                    items.Add(weaponStat.name);
                }
            }
            else
                items.Add(weaponStat.name);
            weaponToString.Add(EnumUtil<Define.Weapon>.Parse(weaponStat.name), weaponStat.name);
        }
        foreach (PassiveStat passiveStat in Managers.Data.PassiveLv[0].Values)
        {
            if(_passiveController.passiveToString.ContainsValue(passiveStat.name))
            {
                if (_passiveController.passiveStat[EnumUtil<Define.Passive>.Parse(passiveStat.name)].level < 8)
                {
                    items.Add(passiveStat.name);
                }
            }
            passiveToString.Add(EnumUtil<Define.Passive>.Parse(passiveStat.name), passiveStat.name);
        }

        // 레벨업 할 수 있는 아이템이 없는 경우, 끝내기
        if (items.Count == 0)
            ClosePopupUI();

        // 아이템 패널 생성
        List<GameObject> ItemPanel = new List<GameObject>();
        for (int i = 0; i < 3; i++)
            ItemPanel.Add(Get<GameObject>(i));

        foreach (GameObject itemPanel in ItemPanel)
        {
            if (items.Count > 0)
            {
                int ran = UnityEngine.Random.Range(0, items.Count);
                string itemName = items[ran];
                items.RemoveAt(ran);

                foreach (Transform child in itemPanel.transform)
                {
                    Managers.Resource.Destroy(child.gameObject);
                }

                GameObject item = Managers.UI.MakeSubItem<UI_LevelUp_Item>(itemPanel.transform).gameObject;
                item.transform.position = itemPanel.transform.position;
                UI_LevelUp_Item invenItem = item.GetOrAddComponent<UI_LevelUp_Item>();
                invenItem.SetInfo(itemName);

                item.BindEvent((PointerEventData) =>
                {
                    if (weaponToString.ContainsValue(itemName))
                        _weaponController.WeaponLevelUp(EnumUtil<Define.Weapon>.Parse(itemName));
                    else if (passiveToString.ContainsValue(itemName))
                        _passiveController.PassiveLevelUp(EnumUtil<Define.Passive>.Parse(itemName));
                    ClosePopupUI();
                });

            }
            else
                Managers.Resource.Destroy(itemPanel.gameObject);
        }
    }

    void OnEnable()
    {
        Time.timeScale = 0f;
    }

    void OnDisable()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    public override void ClosePopupUI()
    {
        base.ClosePopupUI();
    }
    */
}
