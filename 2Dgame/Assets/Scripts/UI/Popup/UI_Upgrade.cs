using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Upgrade : UI_Popup
{
    enum GameObjects
    {
        WeaponUpgradePanel,
        HouseUpgradePanel,
        PlayerUpgradePanel
    }

    public override void Init()
    {
        base.Init();
        /*
        // UI 오브젝트 바인드
        Bind<GameObject>(typeof(GameObjects));

        // 그리드 패널 가져오기
        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);

        // 인벤토리 아이템 (자식 오브젝트) 초기화
        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        // 인벤토리 아이템 생성
        for (int i = 0; i < 3; i++)
        {
            UI_ChSelect_Ch Ch = Managers.UI.MakeSubItem<UI_ChSelect_Ch>(gridPanel.transform);
            Ch.SetInfo((Define.Weapon)i);
        }
        */
    }
}
