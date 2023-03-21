using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBomb : Weapon
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster") || collision.CompareTag("Wall"))
        {
            if (!isDespawn)
            {
                isDespawn = true;
                GameObject fireGround = Managers.Resource.Instantiate("Weapon/FireBomb/FireGround");

                fireGround.GetOrAddComponent<FireGroundInit>().Init(_weaponName, _weaponStat);
                fireGround.transform.position = transform.position;

                Managers.Resource.Destroy(gameObject);
            }
        }
    }
}
