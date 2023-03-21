using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class FireGround : Weapon
{
    bool isGround = false;

    void Update()
    {
        if (!isGround)
            _rb.velocity = Vector2.down * _weaponStat.speed;
    }

    public override void SetStat(Define.Weapon weaponName, WeaponStat weaponStat)
    {
        base.SetStat(weaponName, weaponStat);

        isGround = false;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if(collision.CompareTag("Wall"))
        {
            _rb.velocity = Vector2.zero;
            isGround = true;
        }
    }
}
