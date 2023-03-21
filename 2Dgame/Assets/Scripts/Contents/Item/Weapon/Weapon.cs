using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected bool isTrigger = false;
    protected int _attack = 50;

    protected Define.Weapon _weaponName;
    protected Rigidbody2D _rb;
    protected Transform _player;
    protected PlayerStat _playerStat;
    protected WeaponStat _weaponStat;
    protected bool isDespawn = false;

    void OnEnable()
    {
        isTrigger = false;
    }

    void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        _player = Managers.Game.GetPlayer().transform;
        _playerStat = _player.GetComponent<PlayerStat>();
        _rb = transform.GetComponent<Rigidbody2D>();
    }

    public virtual void SetStat(Define.Weapon weaponName, WeaponStat weaponStat)
    {
        _weaponName = weaponName;
        _weaponStat = WeaponStat.DeepCopy(weaponStat);
        if (_rb == null) Init();
        _rb.velocity = Vector3.zero;
        isDespawn = false;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            if (_weaponStat.num > 0 || _weaponStat.num <= -1)
            {
                EnemyStat enemyStat = collision.transform.parent.GetComponent<EnemyStat>();
                enemyStat.OnDamaged(_weaponStat.attack);
                if(_weaponStat.num > 0) _weaponStat.num--;
            }
            if (_weaponStat.num == 0 && !isDespawn)
            {
                isDespawn = true;
                Managers.Game.Despawn(gameObject);
            }
        }
    }
}
