using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dummyUI_Inven_Item : MonoBehaviour
{
    /*
    // ������ â�� ������ UI
    enum GameObjects
    {
        ItemIcon,
        ItemNameText,
    }

    string _name;

    // ����
    public override void Init()
    {
        // ���� ������Ʈ ���ε�
        Bind<GameObject>(typeof(GameObjects));
    }

    // ������ ����
    public void SetInfo(string name, int level)
    {
        _name = name;
        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<TMP_Text>().text = "+" + level.ToString();
        Get<GameObject>((int)GameObjects.ItemIcon).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Sprites/Items/Weapon/" + name);
    }

    public void SetLevel(int level)
    {
        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<TMP_Text>().text = "+" + level.ToString();
    }

    public void SetImage(string name)
    {
        _name = name;
        Get<GameObject>((int)GameObjects.ItemIcon).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Sprites/Items/Weapon/" + name);
    }
    */
}
