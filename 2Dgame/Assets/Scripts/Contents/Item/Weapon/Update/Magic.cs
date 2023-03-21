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
}
