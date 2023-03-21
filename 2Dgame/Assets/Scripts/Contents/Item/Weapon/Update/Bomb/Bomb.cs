using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Bomb : Weapon
{
    void Update()
    {
        float angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public void Shot()
    {
        if (_rb == null) _rb = transform.GetComponent<Rigidbody2D>();
        _rb.AddForce(transform.right * _weaponStat.speed, ForceMode2D.Impulse);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag("Monster") || collision.CompareTag("Wall")))
        {
            if (!isDespawn)
            {
                isDespawn = true;
                GameObject explosion = Managers.Resource.Instantiate("Weapon/Bomb/Explosion");
                explosion.GetOrAddComponent<Explosion>().SetStat(_weaponName, _weaponStat);
                explosion.transform.position = transform.position;

                Managers.Resource.Destroy(gameObject);
            }
        }
    }
}
