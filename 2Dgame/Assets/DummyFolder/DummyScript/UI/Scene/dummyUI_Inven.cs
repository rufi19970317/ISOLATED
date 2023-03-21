using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyUI_Inven : UI_Scene
{
    /*
    Dictionary<string, GameObject> invenDic = new Dictionary<string, GameObject>();

    // 인벤토리 UI
    enum GameObjects
    {
        WeaponGridPanel,
        PassiveGridPanel
    }

    // 세팅
    public override void Init()
    {
        base.Init();

        // UI 오브젝트 바인드
        Bind<GameObject>(typeof(GameObjects));

        // 그리드 패널 가져오기
        GameObject weaponGridPanel = Get<GameObject>((int)GameObjects.WeaponGridPanel);
        GameObject passiveGridPanel = Get<GameObject>((int)GameObjects.PassiveGridPanel);

        // 인벤토리 아이템 (자식 오브젝트) 초기화
        foreach (Transform child in weaponGridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);
        foreach (Transform child in passiveGridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

    }

    // 인벤토리 아이템 생성
    public void SetWeaponItem(string name, int level)
    {
        GameObject gridPanel = Get<GameObject>((int)GameObjects.WeaponGridPanel);
        if (!invenDic.ContainsKey(name))
        {
            GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform).gameObject;
            UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();
            invenItem.SetInfo(name);
            invenDic.Add(name, item);
        }
    }

    public void SetPassiveItem(string name, int level)
    {
        GameObject gridPanel = Get<GameObject>((int)GameObjects.PassiveGridPanel);
        GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform).gameObject;
        UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();
        invenItem.SetInfo(name, level);
    }
    */
}
