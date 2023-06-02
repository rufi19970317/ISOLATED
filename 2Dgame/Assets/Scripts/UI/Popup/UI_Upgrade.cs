using Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static MoreMountains.CorgiEngine.Weapon;
using static Util;

public class UI_Upgrade : UI_Popup
{
    PlayerStat player;
    WeaponController _weaponController;
    Dictionary<Define.Weapon, string> weaponToString = new Dictionary<Define.Weapon, string>();
    List<string> items = new List<string>();
    int resetCount = 0;

    enum GameObjects
    {
        WeaponUpgradePanel,
        WeaponReset,
        HouseUpgradePanel,
        PlayerUpgradePanel,
        EndButton,
        Close
    }
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public override void Init()
    {
        base.Init();
        // UI 오브젝트 바인드
        Bind<GameObject>(typeof(GameObjects));

        // 그리드 패널 가져오기
        GameObject WeaponPanel = Get<GameObject>((int)GameObjects.WeaponUpgradePanel);
        GameObject HousePanel = Get<GameObject>((int)GameObjects.HouseUpgradePanel);
        GameObject PlayerPanel = Get<GameObject>((int)GameObjects.PlayerUpgradePanel);

        // 인벤토리 아이템 (자식 오브젝트) 초기화
        foreach (Transform child in WeaponPanel.transform)
            Managers.Resource.Destroy(child.gameObject);
        foreach (Transform child in HousePanel.transform)
            Managers.Resource.Destroy(child.gameObject);
        foreach (Transform child in PlayerPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        // 레벨업 할 수 있는 아이템 넣기
        _weaponController = Managers.Game.GetPlayer().GetComponent<WeaponController>();
        player = Managers.Game.GetPlayer().GetComponent<PlayerStat>();

        foreach (WeaponStat weaponStat in Managers.Data.WeaponLv[0].Values)
        {
            if (_weaponController.weaponToString.ContainsValue(weaponStat.name))
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

        foreach (Transform child in WeaponPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }

        for (int i = 0; i < 3; i++)
        {
            SetWeaponUpgrade();
        }

        GameObject resetButton = Get<GameObject>((int)GameObjects.WeaponReset);
        resetButton.GetComponentInChildren<TMP_Text>().text = "x " + ++resetCount;

        resetButton.BindEvent((PointerEventData) =>
        {
            if (player.WeaponUpgradeNum >= resetCount)
            {
                SetWeaponUpgradeItem();
                player.WeaponUpgradeNum -= resetCount;
                resetButton.GetComponentInChildren<TMP_Text>().text = "x " + ++resetCount;
            }
        });

        for (int i = 0; i < 1; i++)
        {
            UI_Upgrade_Item item = Managers.UI.MakeSubItem<UI_Upgrade_Item>(PlayerPanel.transform);
            item.SetInfo(Define.Ability.Player, ((Define.PlayerAbility)i).ToString());
        }

        SetEndButton();

        Get<GameObject>((int)GameObjects.Close).BindEvent((PointerEventData) =>
        {
            Managers.UI.ClosePopupUI(GetComponent<UI_Upgrade>());
        });
    }

    public void SetEndButton()
    {
        if (player.GetKeyUI().GetKeyNum() >= 5)
        {
            if (!Get<GameObject>((int)GameObjects.EndButton).activeSelf)
            {
                Get<GameObject>((int)GameObjects.EndButton).SetActive(true);
                Get<GameObject>((int)GameObjects.EndButton).BindEvent((PointerEventData) =>
                {
                    Managers.Scene.LoadScene(Define.Scene.Start);
                });
            }
        }
        else
        {
            Get<GameObject>((int)GameObjects.EndButton).SetActive(false);
        }
    }

    void SetWeaponUpgrade()
    {
        GameObject WeaponPanel = Get<GameObject>((int)GameObjects.WeaponUpgradePanel);
        
        // 인벤토리 아이템 생성
        if (items.Count > 0)
        {
            int ran = UnityEngine.Random.Range(0, items.Count);
            string itemName = items[ran];
            items.RemoveAt(ran);

            GameObject item = Managers.UI.MakeSubItem<UI_Upgrade_Item_Weapon>(WeaponPanel.transform).gameObject;
            UI_Upgrade_Item_Weapon invenItem = item.GetOrAddComponent<UI_Upgrade_Item_Weapon>();
            if (_weaponController.weaponToString.ContainsValue(itemName))
                invenItem.SetInfo(itemName, _weaponController.weaponsStat[EnumUtil<Define.Weapon>.Parse(itemName)].level + 1);
            else
                invenItem.SetInfo(itemName, 1);

            item.BindEvent((PointerEventData) =>
            {
                if (weaponToString.ContainsValue(itemName))
                {
                    if (_weaponController.weaponToString.ContainsValue(itemName))
                    {
                        if (player.WeaponUpgradeNum >= _weaponController.weaponsStat[EnumUtil<Define.Weapon>.Parse(itemName)].level + 1)
                        {
                            player.WeaponUpgradeNum -= _weaponController.weaponsStat[EnumUtil<Define.Weapon>.Parse(itemName)].level + 1;
                            _weaponController.WeaponLevelUp(EnumUtil<Define.Weapon>.Parse(itemName));
                            SetWeaponUpgradeItem();
                            resetCount = 1;
                            Get<GameObject>((int)GameObjects.WeaponReset).GetComponentInChildren<TMP_Text>().text = "x " + resetCount;
                        }
                    }
                    else
                    {
                        if (player.WeaponUpgradeNum >= 1)
                        {
                            player.WeaponUpgradeNum -= 1;
                            _weaponController.WeaponLevelUp(EnumUtil<Define.Weapon>.Parse(itemName));
                            SetWeaponUpgradeItem();
                            resetCount = 1;
                            Get<GameObject>((int)GameObjects.WeaponReset).GetComponentInChildren<TMP_Text>().text = "x " + resetCount;
                        }
                    }
                }
            });
        }
    }

    void SetWeaponUpgradeItem()
    {
        GameObject WeaponPanel = Get<GameObject>((int)GameObjects.WeaponUpgradePanel);
        items.Clear();
        foreach (WeaponStat weaponStat in Managers.Data.WeaponLv[0].Values)
        {
            if (_weaponController.weaponToString.ContainsValue(weaponStat.name))
            {
                if (_weaponController.weaponsStat[EnumUtil<Define.Weapon>.Parse(weaponStat.name)].level < 8)
                {
                    items.Add(weaponStat.name);
                }
            }
            else
                items.Add(weaponStat.name);
        }

        foreach (Transform child in WeaponPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }

        for (int i = 0; i < 3; i++)
        {
            SetWeaponUpgrade();
        }
    }
}
