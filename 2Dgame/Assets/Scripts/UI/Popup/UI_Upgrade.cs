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
        // UI ������Ʈ ���ε�
        Bind<GameObject>(typeof(GameObjects));

        // �׸��� �г� ��������
        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);

        // �κ��丮 ������ (�ڽ� ������Ʈ) �ʱ�ȭ
        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        // �κ��丮 ������ ����
        for (int i = 0; i < 3; i++)
        {
            UI_ChSelect_Ch Ch = Managers.UI.MakeSubItem<UI_ChSelect_Ch>(gridPanel.transform);
            Ch.SetInfo((Define.Weapon)i);
        }
        */
    }
}
