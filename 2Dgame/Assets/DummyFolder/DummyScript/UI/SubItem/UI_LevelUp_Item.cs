using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_LevelUp_Item : UI_Base
{
    enum GameObjects
    {
        ItemImage,
        ItemName,
        ItemText,
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    public void SetInfo(string item)
    {
        Get<GameObject>((int)GameObjects.ItemImage).GetComponent<Image>().sprite = Managers.Resource.Load<Sprite>("Sprites/Items/weapon/" + item);
        Get<GameObject>((int)GameObjects.ItemName).GetComponent<TMP_Text>().text = item;
        Get<GameObject>((int)GameObjects.ItemText).GetComponent<TMP_Text>().text = item;
    }
}
