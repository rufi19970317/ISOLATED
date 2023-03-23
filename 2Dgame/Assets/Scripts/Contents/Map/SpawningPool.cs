using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    int _monsterCount = 0;

    [SerializeField]
    int _keepMonsterCount = 0;

    [SerializeField]
    Transform[] _spawnPos;

    int _level = 3;
    int[] _monsterType;
    int[] _floor;
    int[] _monsterNum;

    string[] _name = {"Enemy/Enemy-Bee", "Enemy/Enemy-Plant", "Enemy/Enemy-Slug" };

    public void AddMonsterCount(int value) { _monsterCount += value; }
    public void SetKeepMonsterCount(int count) { _keepMonsterCount = count; }

    void Start()
    {
        Managers.Game.OnSpawnEvent -= AddMonsterCount;
        Managers.Game.OnSpawnEvent += AddMonsterCount;
    }

    public void ReserveSpawn()
    {
        if (_level <= Managers.Data.SpawnDict.Count)
        {
            SetSpawn(_level);

            StartCoroutine("SpawnEnemy");
        }
    }

    IEnumerator SpawnEnemy()
    {
        GameObject obj;
        for (int i = 0; i < _monsterType.Length; i++)
        {
            for (int j = 0; j < _monsterNum[i]; j++)
            {
                obj = Managers.Game.Spawn(Define.WorldObject.Monster, _name[_monsterType[i] - 1]);
                obj.transform.position = _spawnPos[_floor[i] * 2 - (j % 2 + 1)].position;
                obj.transform.parent = gameObject.transform;
                
                yield return new WaitForSeconds(0.3f);
            }
        }
    }

    void SetSpawn(int level)
    {
        Data.Spawn data = Managers.Data.SpawnDict[level];

        _monsterType = data.monsterType;
        _floor = data.floor;
        _monsterNum = data.monsterNum;
    }

    public void SetSpawnPos(Transform[] pos)
    {
        _spawnPos = pos;
    }
}
