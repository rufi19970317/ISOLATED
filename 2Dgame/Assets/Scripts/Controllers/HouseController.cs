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
        // 인풋매니저에 이벤트 추가
        Managers.Input.KeyAction -= OnKeyEvent;
        Managers.Input.KeyAction += OnKeyEvent;
    }
    private void OnDisable()
    {
        Managers.Input.KeyAction -= OnKeyEvent;
    }

    public void SetAbility(Define.HouseAbility houseAbility)
    {
        switch(houseAbility)
        {
            case Define.HouseAbility.HouseHeal:
                Managers.Game.GetPlayer().GetComponent<PlayerStat>().HpHeal += 1;
                break;
        }
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
