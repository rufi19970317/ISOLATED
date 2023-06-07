using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ExitBossRoom : UI_Popup
{
    enum GameObjects
    {
        RewardPanel,
        ExitButton
    }
    private void OnEnable()
    {
        Time.timeScale = 0f;
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    public override void Init()
    {
        base.Init();
        // UI ������Ʈ ���ε�
        Bind<GameObject>(typeof(GameObjects));
        SetBoss();
    }

    public void SetBoss()
    {
        GameObject player = Managers.Game.GetPlayer();
        BossRoomSet bossRoomSet = Managers.Game.bossRoomSet;

        GameObject rewardPanel = Get<GameObject>((int)GameObjects.RewardPanel);
        if (bossRoomSet.weaponEXPnum > 0)
            rewardPanel.transform.GetChild(0).GetComponentInChildren<TMP_Text>().text = "x " + bossRoomSet.weaponEXPnum;
        else
            rewardPanel.transform.GetChild(0).gameObject.SetActive(false);

        if (bossRoomSet.houseEXPnum > 0)
            rewardPanel.transform.GetChild(1).GetComponentInChildren<TMP_Text>().text = "x " + bossRoomSet.houseEXPnum;
        else
            rewardPanel.transform.GetChild(1).gameObject.SetActive(false);

        if (bossRoomSet.playerEXPnum > 0)
            rewardPanel.transform.GetChild(2).GetComponentInChildren<TMP_Text>().text = "x " + bossRoomSet.playerEXPnum;
        else
            rewardPanel.transform.GetChild(2).gameObject.SetActive(false);

        Get<GameObject>((int)GameObjects.ExitButton).BindEvent((PointerEventData) =>
        {
            
            Managers.Game.isBossRoom = false;

            // �÷��̾� ��ġ �̵�
            player.transform.position = bossRoomSet.portalPosition.position;
            bossRoomSet.portalPosition.gameObject.SetActive(false);

            player.GetComponent<PlayerStat>().WeaponUpgradeNum += bossRoomSet.weaponEXPnum;
            player.GetComponent<PlayerStat>().HouseUpgradeNum += bossRoomSet.houseEXPnum;
            player.GetComponent<PlayerStat>().PlayerUpgradeNum += bossRoomSet.playerEXPnum;

            // ������ �÷��̾� ����� ����

            // ������ HP UI ����

            // ī�޶� ����
            bossRoomSet.bossCamera.gameObject.SetActive(false);

            Managers.UI.ClosePopupUI(GetComponent<UI_ExitBossRoom>());
        });
    }
}
