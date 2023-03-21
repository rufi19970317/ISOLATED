using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ChSelect_Ch : UI_Base
{
    // 아이템 창의 아이템 UI
    enum GameObjects
    {
        ChIcon,
        ChText,
    }

    Define.Weapon _weaponType;

    // 세팅
    public override void Init()
    {
        // 게임 오브젝트 바인드
        Bind<GameObject>(typeof(GameObjects));
    }

    // 아이템 세팅
    public void SetInfo(Define.Weapon weaponType)
    {
        _weaponType = weaponType;

        // 게임 오브젝트 설정
        Get<GameObject>((int)GameObjects.ChText).GetComponent<Text>().text = _weaponType.ToString();
        // UI 이벤트 실행
        Get<GameObject>((int)GameObjects.ChIcon).BindEvent((PointerEventData) =>
        {
            Managers.Scene.LoadScene(Define.Scene.Game);
        });
    }
}
