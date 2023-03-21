using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    PlayerController _player;
    PlayerCollider _coll;

    private Animator anim;
    [HideInInspector]
    public SpriteRenderer sr;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        _player = GetComponentInParent<PlayerController>();
        _coll = _player.transform.GetComponent<PlayerCollider>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        anim.SetBool("onGround", _coll.onGround);
        anim.SetBool("onWall", _coll.onWall);
        anim.SetBool("onRightWall", _coll.onRightWall);
        anim.SetBool("wallSlide", _player.IsSliding);
        anim.SetBool("isDashing", _player.IsDashing);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("WallSlide"))
            sr.flipX = _coll.onRightWall;
        else
            sr.flipX = false;
    }

    public void SetHorizontalMovement(Vector2 dir, float yVel)
    {
        if (anim == null) Init();
        anim.SetFloat("HorizontalAxis", dir.x);
        anim.SetFloat("VerticalAxis", dir.y);
        anim.SetFloat("VerticalVelocity", yVel);
    }

    public void SetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }
}
