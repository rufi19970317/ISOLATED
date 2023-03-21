using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inven_Item : UI_Base
{
    // ������ â�� ������ UI
    enum GameObjects
    {
        ItemIcon
    }

    
    // ����
    public override void Init()
    {
        // ���� ������Ʈ ���ε�
        Bind<GameObject>(typeof(GameObjects));
    }

    // ������ ����
    public void SetImage(Sprite image)
    {
        Get<GameObject>((int)GameObjects.ItemIcon).GetComponent<Image>().sprite = image;
    }
}
