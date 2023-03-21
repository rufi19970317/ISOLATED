using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inven_Item : UI_Base
{
    // 아이템 창의 아이템 UI
    enum GameObjects
    {
        ItemIcon
    }

    
    // 세팅
    public override void Init()
    {
        // 게임 오브젝트 바인드
        Bind<GameObject>(typeof(GameObjects));
    }

    // 아이템 세팅
    public void SetImage(Sprite image)
    {
        Get<GameObject>((int)GameObjects.ItemIcon).GetComponent<Image>().sprite = image;
    }
}
