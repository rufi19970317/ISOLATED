using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Weapon
{
    void Update()
    {
        _rb.velocity = transform.right * _weaponStat.speed;
    }
}
