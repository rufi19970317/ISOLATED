using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : Stat
{
    bool isRespawn = false;
    [SerializeField]
    Define.EXPType _expType = Define.EXPType.WeaponEXP;
    MonsterController monsterController;

    SpawningPool _pool;
    bool isDefenseMonster = false;

    void Start()
    {
        Init();
    }

    void Init()
    {
        isRespawn = false;

        if(GetComponent<MonsterController>() != null)
            monsterController = GetComponent<MonsterController>();


        if (_expType == Define.EXPType.HouseEXP)
        {
            if (gameObject.GetComponentInChildren<UI_HPBar_Object>() == null)
            {
                UI_HPBar_Object hpBar = Managers.UI.MakeWorldSpaceUI<UI_HPBar_Object>(transform);
                hpBar.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

            }
        }

        _hp = 100;
        _maxHp = 100;
        _attack = 5;
        _moveSpeed = 5f;
    }

    void OnEnable()
    {
        Init();
    }

    public void SetDefenseEnemy(SpawningPool pool)
    {
        _pool = pool;
        isDefenseMonster = true;
    }

    // Update is called once per frame
    public override void OnDamaged(int damage)
    {
        int _damage = Mathf.Max(0, damage);
        Hp -= _damage;

        if (Hp > 0)
        {
            if (monsterController != null)
                monsterController.OnDamaged();
        }

        if (Hp <= 0)
        {
            OnDead();
        }
    }

    protected override void OnDead()
    {
        if (!isRespawn)
        {
            Hp = 0;
            var expObj = Managers.Game.Spawn(Define.WorldObject.Item, "Item/" + _expType.ToString());
            expObj.transform.position = transform.position;

            if (isDefenseMonster)
            {
                _pool.AddMonsterCount();
            }
            isDefenseMonster = false;

            Managers.Game.Spawn(Define.WorldObject.Unknown, "Enemy/Death").transform.position = transform.position;
            Managers.Game.Despawn(gameObject);
            isRespawn = true;
        }
    }
}
