using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct BossRoomSet
{
    public string bossName;

    public int requireEXPnum;

    public int weaponEXPnum;
    public int houseEXPnum;
    public int playerEXPnum;

    public Transform portalPosition;
    public Transform roomPosition;
    public Transform bossCamera;
}

public class UI_EnterBossRoom : UI_Popup
{
    enum GameObjects
    {
        BossScreen,
        RequirementsPanel,
        RewardPanel,
        EnterButton,
        Close
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
        // UI 오브젝트 바인드
        Bind<GameObject>(typeof(GameObjects));
    }

    public void SetBoss(BossRoomSet bossRoomSet)
    {
        GameObject player = Managers.Game.GetPlayer();

        GameObject bossScreen = Get<GameObject>((int)GameObjects.BossScreen);
        bossScreen.GetComponentInChildren<TMP_Text>().text = bossRoomSet.bossName;
        bossScreen.GetComponentInChildren<Image>().sprite = Managers.Resource.Load<Sprite>("Sprites/Boss/" + bossRoomSet.bossName);

        Get<GameObject>((int)GameObjects.RequirementsPanel).GetComponentInChildren<TMP_Text>().text
            = player.GetComponent<PlayerStat>().WeaponUpgradeNum + " / "+ bossRoomSet.requireEXPnum;

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

        Managers.Game.bossRoomSet = bossRoomSet;

        Get<GameObject>((int)GameObjects.EnterButton).BindEvent((PointerEventData) =>
        {
            if(player.GetComponent<PlayerStat>().WeaponUpgradeNum >= bossRoomSet.requireEXPnum)
            {
                Managers.Game.isBossRoom = true;
                
                // 플레이어 위치 이동
                player.transform.position = bossRoomSet.roomPosition.position;
                player.GetComponent<PlayerStat>().WeaponUpgradeNum -= bossRoomSet.requireEXPnum;
                // 보스용 플레이어 무기로 변경

                // 보스용 HP UI 생성

                // 카메라 변경
                bossRoomSet.bossCamera.gameObject.SetActive(true);
                bossRoomSet.bossCamera.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;

                Managers.UI.ClosePopupUI(GetComponent<UI_EnterBossRoom>());
            }
        });

        Get<GameObject>((int)GameObjects.Close).BindEvent((PointerEventData) =>
        {
            Managers.UI.ClosePopupUI(GetComponent<UI_EnterBossRoom>());
        });
    }
}
