using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dummyUI_Inven_Item : MonoBehaviour
{
    /*
    // 아이템 창의 아이템 UI
    enum GameObjects
    {
        ItemIcon,
        ItemNameText,
    }

    string _name;

    // 세팅
    public override void Init()
    {
        // 게임 오브젝트 바인드
        Bind<GameObject>(typeof(GameObjects));
    }

    // 아이템 세팅
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
