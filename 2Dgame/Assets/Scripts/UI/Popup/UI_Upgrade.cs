using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Upgrade : UI_Popup
{
    enum GameObjects
    {
        WeaponUpgradePanel,
        HouseUpgradePanel,
        PlayerUpgradePanel,
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

        // 인벤토리 아이템 생성
        for (int i = 0; i < 8; i++)
        {
            UI_Upgrade_Item item  = Managers.UI.MakeSubItem<UI_Upgrade_Item>(WeaponPanel.transform);
            item.SetInfo(Define.Ability.Weapon, ((Define.Weapon)i).ToString());
        }
        for(int i = 0; i < 1; i++)
        {
            UI_Upgrade_Item item = Managers.UI.MakeSubItem<UI_Upgrade_Item>(PlayerPanel.transform);
            item.SetInfo(Define.Ability.Player, ((Define.PlayerAbility)i).ToString());
        }

        Get<GameObject>((int)GameObjects.Close).BindEvent((PointerEventData) =>
        {
            Managers.UI.ClosePopupUI(GetComponent<UI_Upgrade>());
        });
    }
}
