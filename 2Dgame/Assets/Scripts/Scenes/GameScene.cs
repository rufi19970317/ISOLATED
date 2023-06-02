using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameScene : BaseScene
{
    [SerializeField]
    Transform PlayerSpawnPos;


    CinemachineVirtualCamera cvcCamera;

    string _playerName = "ch01";

    // 세팅
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        // 데이터 가져오기
        //Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        //gameObject.GetOrAddComponent<CursorController>();
        //Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Player/" + _playerName);
        player.transform.position = PlayerSpawnPos.position;
        
        // 무기 설정
        player.GetComponent<PlayerStat>().SetStat();

        cvcCamera = FindObjectOfType<CinemachineVirtualCamera>();
        cvcCamera.Follow = player.transform;
    }

    public void SetPlayerName(string name)
    {
        _playerName = name;
    }

    public override void Clear()
    {
    
    }
}
