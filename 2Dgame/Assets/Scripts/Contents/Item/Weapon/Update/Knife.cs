using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    PlayerCollider playerColl;

    protected override void Init()
    {
        base.Init();

        playerColl = _player.GetComponent<PlayerCollider>();
    }

    void Update()
    {
        if (_rb.velocity.x == 0)
        {
            if (playerColl.onRightWall && !playerColl.onGround)
                _rb.velocity = new Vector2(-_player.localScale.x * _weaponStat.speed, 0f);
            else
                _rb.velocity = new Vector2(_player.localScale.x * _weaponStat.speed, 0f);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (collision.CompareTag("Wall"))
        {
            if (!isDespawn)
            {
                Managers.Game.Despawn(gameObject);
                isDespawn = true;
            }
        }
    }
}
