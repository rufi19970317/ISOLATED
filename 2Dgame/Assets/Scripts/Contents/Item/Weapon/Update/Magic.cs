using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : Weapon
{
    float _weaponTime = 0f;

    void Update()
    {
        _weaponTime -= Time.deltaTime;
        if (_weaponTime <= 0f)
        {
            Managers.Resource.Destroy(gameObject);
        }
    }

    public override void SetStat(Define.Weapon weaponName, WeaponStat weaponStat)
    {
        base.SetStat(weaponName, weaponStat);

        _weaponTime = weaponStat.duration;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.transform.parent.GetComponent<EnemyStat>().weaponTime[(int)_weaponName] = 0f;
        }
    }
    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            EnemyStat enemyStat = collision.transform.parent.GetComponent<EnemyStat>();
            enemyStat.isDamage[(int)_weaponName] = true;

            if (enemyStat.weaponTime[(int)_weaponName] <= 0f)
            {
                enemyStat.weaponTime[(int)_weaponName] = 0.5f;
                if (_weaponStat.num > 0 || _weaponStat.num <= -1)
                {
                    enemyStat = collision.transform.parent.GetComponent<EnemyStat>();
                    enemyStat.OnDamaged(_weaponStat.attack);
                    if (_weaponStat.num > 0) _weaponStat.num--;
                }
                if (_weaponStat.num == 0 && !isDespawn)
                {
                    isDespawn = true;
                    Managers.Game.Despawn(gameObject);
                }
            }
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.transform.parent.GetComponent<EnemyStat>().isDamage[(int)Define.Weapon.Hook] = false;
        }
    }
}
