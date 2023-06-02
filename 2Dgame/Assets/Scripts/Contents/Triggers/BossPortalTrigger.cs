using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossPortalTrigger : MonoBehaviour
{
    [SerializeField]
    BossRoomSet bossRoomSet;

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
        if (Time.timeScale != 0f && isPlayer && !Managers.Game.isDefense)
        {
            switch (Key)
            {
                case Define.KeyEvent.Up:
                    UI_EnterBossRoom bossRoomUI = Managers.UI.ShowPopUpUI<UI_EnterBossRoom>();
                    bossRoomUI.SetBoss(bossRoomSet);
                    break;
            }
        }
        else
        {

        }
    }
}
