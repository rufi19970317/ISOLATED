using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    Stat _stat;

    [SerializeField]
    GameObject _player;

    SpriteRenderer spriteRenderer;

    [SerializeField]
    Define.MonsterType _type;

    Vector2 _destPosGround;
    Vector2 _destPosFly;

    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        WorldObjectType = Define.WorldObject.Monster;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer == null) spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
        _stat = gameObject.GetComponent<EnemyStat>();
    }

    void Update()
    {
        if(_player == null)
        {
            SetPlayer();
        }

        UpdateMoving();
    }

    void SetPlayer()
    {
        GameObject player = Managers.Game.GetPlayer();
        if (player == null)
            return;
        
        _player = player;
    }


    void UpdateMoving()
    {
        if (_player != null)
        {
            _destPosGround = new Vector2(_player.transform.position.x, transform.position.y);
            _destPosFly = _player.transform.position;
        }

        // 몬스터 이동
        // 플레이어 방향으로, 스피드만큼
        Vector2 dir = Vector2.zero;
        switch (_type)
        {
            case Define.MonsterType.Ground:
                dir = _destPosGround - (Vector2)transform.position;
                break;
            case Define.MonsterType.Fly:
                dir = _destPosFly - (Vector2)transform.position;
                break;
            default:
                break;
        }
        if (dir.magnitude >= 0.1f)
        {
            transform.position += (Vector3)dir.normalized * _stat.MoveSpeed * Time.deltaTime;
            if (dir.normalized.x < 0) spriteRenderer.flipX = false;
            else spriteRenderer.flipX = true;
        }
    }
}
