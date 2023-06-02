using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour
{
    bool isPlayer = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayer = false;
        }
    }

    private void OnEnable()
    {
        // ��ǲ�Ŵ����� �̺�Ʈ �߰�
        Managers.Input.KeyAction -= OnKeyEvent;
        Managers.Input.KeyAction += OnKeyEvent;
    }
    private void OnDisable()
    {
        Managers.Input.KeyAction -= OnKeyEvent;
    }

    void OnKeyEvent(Define.KeyEvent Key)
    {
        if (Time.timeScale != 0f && isPlayer)
        {
            switch (Key)
            {
                case Define.KeyEvent.Up:
                    UI_Upgrade upgradeUI = Managers.UI.ShowPopUpUI<UI_Upgrade>();
                    upgradeUI.SetEndButton();
                    break;
            }
        }
        else
        {

        }
    }
}
