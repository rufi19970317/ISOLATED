using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameScene : BaseScene
{
    [SerializeField]
    Transform[] SpawnPos;
    [SerializeField]
    Transform PlayerSpawnPos;

    float _time = 0f;

    SpawningPool _pool;
    UI_Timer Timer;

    CinemachineVirtualCamera cvcCamera;

    string _playerName = "ch01";

    // 세팅
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;
        Timer = Managers.UI.ShowSceneUI<UI_Timer>();

        // 데이터 가져오기
        //Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        //gameObject.GetOrAddComponent<CursorController>();
        //Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Player/" + _playerName);
        player.transform.position = PlayerSpawnPos.position;
        
        // 무기 설정
        player.GetComponent<PlayerStat>().SetStat();

        GameObject pl = new GameObject { name = "SpawningPool" };
        _pool = pl.GetOrAddComponent<SpawningPool>();
        _pool.SetSpawnPos(SpawnPos);
        _pool.ReserveSpawn();

        cvcCamera = FindObjectOfType<CinemachineVirtualCamera>();
        cvcCamera.Follow = player.transform;
    }

    void Update()
    {
        _time = Time.deltaTime;
        SetTimer();
        SpawnEnemy();
    }

    float _second = 0f;
    int _minute = 0;
    void SetTimer()
    {
        _second += _time;
        if ((int)_second >= 60f)
        {
            _second = 0f;
            _minute++;
        }

        Timer.SetTimer(_minute, (int)_second);
    }

    float _spawnTime = 0f;
    void SpawnEnemy()
    {
        _spawnTime += _time;
        if((int)_spawnTime >= 60f)
        {
            _spawnTime = 0f;
            _pool.ReserveSpawn();
        }
    }

    public void SetPlayerName(string name)
    {
        _playerName = name;
    }

    public override void Clear()
    {
    
    }
}
