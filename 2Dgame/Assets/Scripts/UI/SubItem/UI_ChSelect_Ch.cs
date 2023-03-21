using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ChSelect_Ch : UI_Base
{
    // ������ â�� ������ UI
    enum GameObjects
    {
        ChIcon,
        ChText,
    }

    Define.Weapon _weaponType;

    // ����
    public override void Init()
    {
        // ���� ������Ʈ ���ε�
        Bind<GameObject>(typeof(GameObjects));
    }

    // ������ ����
    public void SetInfo(Define.Weapon weaponType)
    {
        _weaponType = weaponType;

        // ���� ������Ʈ ����
        Get<GameObject>((int)GameObjects.ChText).GetComponent<Text>().text = _weaponType.ToString();
        // UI �̺�Ʈ ����
        Get<GameObject>((int)GameObjects.ChIcon).BindEvent((PointerEventData) =>
        {
            Managers.Scene.LoadScene(Define.Scene.Game);
        });
    }
}
