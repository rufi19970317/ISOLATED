using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummyUI_Inven : UI_Scene
{
    /*
    Dictionary<string, GameObject> invenDic = new Dictionary<string, GameObject>();

    // �κ��丮 UI
    enum GameObjects
    {
        WeaponGridPanel,
        PassiveGridPanel
    }

    // ����
    public override void Init()
    {
        base.Init();

        // UI ������Ʈ ���ε�
        Bind<GameObject>(typeof(GameObjects));

        // �׸��� �г� ��������
        GameObject weaponGridPanel = Get<GameObject>((int)GameObjects.WeaponGridPanel);
        GameObject passiveGridPanel = Get<GameObject>((int)GameObjects.PassiveGridPanel);

        // �κ��丮 ������ (�ڽ� ������Ʈ) �ʱ�ȭ
        foreach (Transform child in weaponGridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);
        foreach (Transform child in passiveGridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

    }

    // �κ��丮 ������ ����
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
