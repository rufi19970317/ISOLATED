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

    Rigidbody2D rigid;
    Animator anim;

    bool isDamaged = false;

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
        rigid = GetComponent<Rigidbody2D>();

        if(GetComponent<Animator>() != null)
            anim = GetComponent<Animator>();
        else
            anim = GetComponentInChildren<Animator>();

    }

    void OnEnable()
    {
        isDamaged = false;
        if(spriteRenderer!= null)
            spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void Update()
    {
        if(_player == null)
        {
            SetPlayer();
        }
    }

    void FixedUpdate()
    {
        if (!isDamaged)
            UpdateMoving();
        else
            StartCoroutine(Knockback());
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

        Vector2 nextVec = dir.normalized * _stat.MoveSpeed * Time.fixedDeltaTime;
        

        if (dir.magnitude >= 0.1f)
        {
            rigid.MovePosition(rigid.position + nextVec);
            rigid.velocity = Vector2.zero;

            if (dir.normalized.x < 0) spriteRenderer.flipX = false;
            else spriteRenderer.flipX = true;
        }
    }

    public void OnDamaged()
    {
        isDamaged = true;
    }

    IEnumerator Knockback()
    {
        spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 1);

        Vector3 playerPos = Managers.Game.GetPlayer().transform.position;
        
        _destPosGround = new Vector2(_player.transform.position.x, transform.position.y);
        _destPosFly = _player.transform.position;
        
        Vector3 dirVect = transform.position - playerPos;
        switch (_type)
        {
            case Define.MonsterType.Ground:
                dirVect = (Vector2)transform.position - _destPosGround;
                break;
            case Define.MonsterType.Fly:
                dirVect = (Vector2)transform.position - _destPosFly;
                break;
            default:
                break;
        }
        rigid.AddForce(dirVect.normalized, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.1f);

        isDamaged = false;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
