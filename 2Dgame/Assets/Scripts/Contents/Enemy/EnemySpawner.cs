using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    Transform[] SpawnPos;

    float _time = 0f;

    SpawningPool _pool;
    UI_Timer Timer;
    int monsterTotalNum;
    bool isDefense = false;
    const float _spawnTime = 60f;

    void Start()
    {
        Init();
    }

    void Init()
    {
        Timer = Managers.UI.ShowSceneUI<UI_Timer>();

        GameObject pl = new GameObject { name = "SpawningPool" };
        _pool = pl.GetOrAddComponent<SpawningPool>();
        _pool.SetSpawnPos(SpawnPos);
        SpawnEnemy();
    }

    void Update()
    {
        if (!isDefense && !Managers.Game.isBossRoom)
        {
            _time -= Time.deltaTime;
            Timer.SetTimer(_time / _spawnTime);

            if (_time <= 0f)
            {
                SpawnEnemy();
            }
        }
        else if (isDefense)
        {
            Timer.SetCount(_pool.GetDfMonsterCount() + " / " + monsterTotalNum);

            if (_pool.GetDfMonsterCount() >= monsterTotalNum)
            {
                SetTimer();
            }
        }
    }

    void SetTimer()
    {
        isDefense = false;
        Timer.SetObject(isDefense);
        Timer.SetTimer(_time / _spawnTime);
        Managers.Game.isDefense = false;
    }

    void SpawnEnemy()
    {
        monsterTotalNum = _pool.ReserveSpawn();
        isDefense = true;
        _time = _spawnTime;
        Timer.SetObject(isDefense);
        Timer.SetCount(_pool.GetDfMonsterCount() + " / " + monsterTotalNum);
        Managers.Game.isDefense = true;
    }
}
