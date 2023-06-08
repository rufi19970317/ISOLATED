using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackTrigger : MonoBehaviour
{
    GameObject _player;
    PlayerStat _targetStat;
    Stat _stat;

    bool isDamage = false;
    float _damageTime = 0f;
    float _damageSpeed = 0.8f;

    bool isHouseDamage = false;
    float _houseDamageTime = 0f;
    float _houseDamageSpeed = 0.8f;

    void Start()
    {
        if (Managers.Game.GetPlayer() != null)
        {
            _player = Managers.Game.GetPlayer();
            _targetStat = _player.GetComponent<PlayerStat>();
        }
        _stat = transform.parent.GetComponent<Stat>();
        _damageSpeed = 0.8f;
        _damageTime = _damageSpeed;

        _houseDamageSpeed = 0.8f;
        _houseDamageTime = _houseDamageSpeed;
    }

    void Update()
    {
        if(_player == null)
        {
            if (Managers.Game.GetPlayer() != null)
            {
                _player = Managers.Game.GetPlayer();
                _targetStat = _player.GetComponent<PlayerStat>();
            }
        }
        if (isDamage)
            _damageTime -= Time.deltaTime;

        if (isHouseDamage)
            _houseDamageTime -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _damageTime = 0f;
        }

        if (collision.CompareTag("House"))
        {
            _houseDamageTime = 0f;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isDamage = true;

            if (_damageTime <= 0f)
            {
                _targetStat.OnDamaged(_stat.Attack);
                _damageTime = _damageSpeed;
            }
        }
        if (collision.CompareTag("House"))
        {
            isHouseDamage = true;

            if (_houseDamageTime <= 0f)
            {
                if (collision.GetComponent<HouseStat>().Hp > 0)
                {
                    collision.GetComponent<HouseStat>().OnDamaged(_stat.Attack);
                    _houseDamageTime = _houseDamageSpeed;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isDamage = false;
        }

        if (collision.CompareTag("House"))
        {
            isHouseDamage = false;
        }
    }
}
