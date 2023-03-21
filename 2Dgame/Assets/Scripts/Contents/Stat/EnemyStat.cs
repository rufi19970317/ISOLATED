using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : Stat
{
    bool isRespawn = false;
    [SerializeField]
    Define.EXPType _expType = Define.EXPType.WeaponEXP;

    void Start()
    {
        Init();
    }

    void Init()
    {
        isRespawn = false;
        _hp = 10;
        _maxHp = 10;
        _attack = 10;
        _moveSpeed = 5f;
    }

    void OnEnable()
    {
        Init();
    }

    // Update is called once per frame
    public override void OnDamaged(int damage)
    {
        int _damage = Mathf.Max(0, damage);
        Hp -= _damage;

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
            Managers.Game.Despawn(gameObject);
            isRespawn = true;
        }
    }
}
