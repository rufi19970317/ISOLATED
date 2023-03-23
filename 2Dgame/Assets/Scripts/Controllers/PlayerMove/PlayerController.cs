using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    #region Player Ability
    bool _abilityDash = false;
    bool _abilityRun = false;

    public void SetAbility(Define.PlayerAbility ability)
    {
        switch(ability)
        {
            case Define.PlayerAbility.Dash:
                _abilityDash = true;
                break;
            case Define.PlayerAbility.Run:
                _abilityRun = true;
                break;
        }
    }
    #endregion

    #region SCRIPT PARAMETERS
    public PlayerData _playerData;
    PlayerCollider _coll;
    PlayerStat _stat;
    AnimationScript anim = null;
    Rigidbody2D rb = null;
    SpriteRenderer spriteRenderer;
    GhostTrail _ghost;

    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;
    #endregion

    #region Init
    void Start()
    {
        Init();
    }

    public void Init()
    {
        WorldObjectType = Define.WorldObject.Player;
        _stat = gameObject.GetComponent<PlayerStat>();
        anim = GetComponentInChildren<AnimationScript>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _coll = GetComponent<PlayerCollider>();

        SetGravityScale(_playerData.gravityScale);
        IsFacingRight = true;

        _ghost = Managers.Resource.Instantiate("Player/Ghost").GetOrAddComponent<GhostTrail>();
    }


    #region AddEvent
    private void OnEnable()
    {
        // 인풋매니저에 이벤트 추가
        Managers.Input.KeyAction -= OnKeyEvent;
        Managers.Input.KeyAction += OnKeyEvent;
    }
    private void OnDisable()
    {
        Managers.Input.KeyAction -= OnKeyEvent;
    }
    #endregion

    #endregion

    #region Dead event
    public void OnDeadEvent()
    {
        Managers.Game.Despawn(gameObject);
        Managers.UI.ShowPopUpUI<UI_GameOver>();
    }

    void OnDie()
    {
        rb.bodyType = RigidbodyType2D.Static;
    }
    #endregion

    #region Update
    void Update()
    {
        UpdateMovement();
        if(Time.timeScale != 0f)
            anim.SetHorizontalMovement(_moveInput, rb.velocity.y);
    }

    private void FixedUpdate()
    {
        //Handle Run
        if (!IsDashing)
        {
            if (IsWallJumping)
                Run(_playerData.wallJumpRunLerp);
            else
                Run(1);
        }
        else if (_isDashAttacking)
        {
            Run(_playerData.dashEndRunLerp);
        }

        //Handle Slide
        if (IsSliding)
            Slide();
    }


    void UpdateMovement()
    {
        #region TIMERS
        LastOnGroundTime -= Time.deltaTime;
        LastOnWallTime -= Time.deltaTime;
        LastOnWallRightTime -= Time.deltaTime;
        LastOnWallLeftTime -= Time.deltaTime;

        LastPressedJumpTime -= Time.deltaTime;
        LastPressedDashTime -= Time.deltaTime;
        #endregion

        #region COLLISION CHECKS
        if(_coll.onGround && !_groundTouch)
        {
            jumpParticle.Play();
            _groundTouch = true;
        }
        if(!_coll.onGround && _groundTouch)
        {
            _groundTouch = false;
        }

        if (!IsDashing && !IsJumping)
        {
            //Ground Check
            if (_coll.onGround && !IsJumping) //checks if set box overlaps with ground
            {
                LastOnGroundTime = _playerData.coyoteTime; //if so sets the lastGrounded to coyoteTime
            }

            //Right Wall Check
            if (_coll.isRightWall && !IsWallJumping)
                LastOnWallRightTime = _playerData.coyoteTime;

            //Right Wall Check
            if (_coll.isLeftWall && !IsWallJumping)
                LastOnWallLeftTime = _playerData.coyoteTime;

            //Two checks needed for both left and right walls since whenever the play turns the wall checkPoints swap sides
            LastOnWallTime = Mathf.Max(LastOnWallLeftTime, LastOnWallRightTime);
        }
        #endregion

        #region JUMP CHECKS
        if (IsJumping && rb.velocity.y < 0)
        {
            IsJumping = false;

            if (!IsWallJumping)
                _isJumpFalling = true;
        }

        if (IsWallJumping && Time.time - _wallJumpStartTime > _playerData.wallJumpTime)
        {
            IsWallJumping = false;
        }

        if (LastOnGroundTime > 0 && !IsJumping && !IsWallJumping)
        {
            _isJumpCut = false;

            if (!IsJumping)
                _isJumpFalling = false;
        }

        if (!IsDashing)
        {
            //Jump
            if (CanJump() && LastPressedJumpTime > 0)
            {
                IsJumping = true;
                IsWallJumping = false;
                _isJumpCut = false;
                _isJumpFalling = false;
                Jump();
            }
            //WALL JUMP
            else if (CanWallJump() && LastPressedJumpTime > 0)
            {
                IsWallJumping = true;
                IsJumping = false;
                _isJumpCut = false;
                _isJumpFalling = false;

                _wallJumpStartTime = Time.time;
                _lastWallJumpDir = (LastOnWallRightTime > 0) ? -1 : 1;

                WallJump(_lastWallJumpDir);
            }
        }
        #endregion

        #region DASH CHECKS
        if (_abilityDash && CanDash() && LastPressedDashTime > 0)
        {
            //If not direction pressed, dash forward
            if (_moveInput != Vector2.zero)
                _lastDashDir = _moveInput;
            else
                _lastDashDir = IsFacingRight ? Vector2.right : Vector2.left;

            IsDashing = true;
            IsJumping = false;
            IsWallJumping = false;
            _isJumpCut = false;

            StartCoroutine(nameof(StartDash), _lastDashDir);
        }
        #endregion

        #region SLIDE CHECKS
        if (CanSlide() && ((LastOnWallLeftTime > 0 && _moveInput.x < 0) || (LastOnWallRightTime > 0 && _moveInput.x > 0)))
            IsSliding = true;
        else
            IsSliding = false;
        WallParticle(_moveInput.y);
        #endregion

        #region GRAVITY
        if (!_isDashAttacking)
        {
            //Higher gravity if we've released the jump input or are falling
            if (IsSliding)
            {
                SetGravityScale(0);
            }
            else if (rb.velocity.y < 0 && _moveInput.y < 0)
            {
                //Much higher gravity if holding down
                SetGravityScale(_playerData.gravityScale * _playerData.fastFallGravityMult);
                //Caps maximum fall speed, so when falling over large distances we don't accelerate to insanely high speeds
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -_playerData.maxFastFallSpeed));
            }
            else if (_isJumpCut)
            {
                //Higher gravity if jump button released
                SetGravityScale(_playerData.gravityScale * _playerData.jumpCutGravityMult);
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -_playerData.maxFallSpeed));
            }
            else if ((IsJumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(rb.velocity.y) < _playerData.jumpHangTimeThreshold)
            {
                SetGravityScale(_playerData.gravityScale * _playerData.jumpHangGravityMult);
            }
            else if (rb.velocity.y < 0)
            {
                //Higher gravity if falling
                SetGravityScale(_playerData.gravityScale * _playerData.fallGravityMult);
                //Caps maximum fall speed, so when falling over large distances we don't accelerate to insanely high speeds
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -_playerData.maxFallSpeed));
            }
            else
            {
                //Default gravity if standing on a platform or moving upwards
                SetGravityScale(_playerData.gravityScale);
            }
        }
        else
        {
            //No gravity when dashing (returns to normal once initial dashAttack phase over)
            SetGravityScale(0);
        }
        #endregion
    }
    #endregion

    #region Particle
    void WallParticle(float vertical)
    {
        var main = slideParticle.main;

        if (IsSliding)
        {
            slideParticle.transform.parent.localScale = new Vector3(ParticleSide(), 1, 1);
            main.startColor = Color.white;
        }
        else
        {
            main.startColor = Color.clear;
        }
    }

    int ParticleSide()
    {
        int particleSide = _coll.onRightWall ? 1 : -1;
        return particleSide;
    }
    #endregion

    #region KeyEvent
    void OnKeyEvent(Define.KeyEvent Key)
    {
        if (Time.timeScale != 0f)
        {
            switch (Key)
            {
                case Define.KeyEvent.Stop:
                    _moveInput = Vector2.zero;
                    break;

                case Define.KeyEvent.Left:
                case Define.KeyEvent.Right:
                    OnMovingEvent(Key);
                    break;

                case Define.KeyEvent.Jump:
                    OnJumpEvent();
                    break;
                case Define.KeyEvent.JumpUp:
                    OnJumpUpEvent();
                    break;

                case Define.KeyEvent.Up:
                    _moveInput.y = 1f;
                    break;

                case Define.KeyEvent.Down:
                    _moveInput.y = -1f;
                    break;

                // TODO : UI?
                case Define.KeyEvent.Select:
                    break;

                case Define.KeyEvent.Cancel:
                    OnDashEvent();
                    break;
            }
        }
        else
        {
            switch (Key)
            {
                case Define.KeyEvent.Stop:
                    _moveInput = Vector2.zero;
                    break;
            }
        }
    }

    // 플레이어 이동
    void OnMovingEvent(Define.KeyEvent Key)
    {
        CheckDirectionToFace(Key == Define.KeyEvent.Right);
        _moveInput.x = transform.localScale.x;
    }

    void OnJumpEvent()
    {
        LastPressedJumpTime = _playerData.jumpInputBufferTime;
        anim.SetTrigger("jump");
    }

    void OnJumpUpEvent()
    {
        if (CanJumpCut() || CanWallJumpCut())
            _isJumpCut = true;
    }

    void OnDashEvent()
    {
        LastPressedDashTime = _playerData.dashInputBufferTime;
    }
    #endregion

    #region STATE PARAMETERS
    //Variables control the various actions the player can perform at any time.
    //These are fields which can are public allowing for other sctipts to read them
    //but can only be privately written to.

    public bool IsFacingRight { get; private set; }
    public bool IsJumping { get; private set; }
    public bool IsWallJumping { get; private set; }
    public bool IsDashing { get; private set; }
    public bool IsSliding { get; private set; }

    //Timers (also all fields, could be private and a method returning a bool could be used)
    public float LastOnGroundTime { get; private set; }
    public float LastOnWallTime { get; private set; }
    public float LastOnWallRightTime { get; private set; }
    public float LastOnWallLeftTime { get; private set; }

    //Jump
    bool _groundTouch;
    private bool _isJumpCut;
    private bool _isJumpFalling;

    //Wall Jump
    private float _wallJumpStartTime;
    private int _lastWallJumpDir;

    //Dash
    private int _dashesLeft;
    private bool _dashRefilling;
    private Vector2 _lastDashDir;
    private bool _isDashAttacking;

    #endregion

    #region INPUT PARAMETERS

    private Vector2 _moveInput;
    public float LastPressedJumpTime { get; private set; }
    public float LastPressedDashTime { get; private set; }
    #endregion

    #region LAYERS & TAGS
    [Space]
    [Header("Polish")]
    public ParticleSystem dashParticle;
    public ParticleSystem jumpParticle;
    public ParticleSystem wallJumpParticle;
    public ParticleSystem slideParticle;
    #endregion

    #region GENERAL METHODS
    public void SetGravityScale(float scale)
    {
        rb.gravityScale = scale;
    }
    #endregion

    #region RUN METHODS
    private void Run(float lerpAmount)
    {
        //Calculate the direction we want to move in and our desired velocity
        float targetSpeed = _moveInput.x * _playerData.runMaxSpeed;
        //We can reduce are control using Lerp() this smooths changes to are direction and speed
        targetSpeed = Mathf.Lerp(rb.velocity.x, targetSpeed, lerpAmount);

        #region Calculate AccelRate
        float accelRate;

        //Gets an acceleration value based on if we are accelerating (includes turning) 
        //or trying to decelerate (stop). As well as applying a multiplier if we're air borne.
        if (LastOnGroundTime > 0)
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _playerData.runAccelAmount : _playerData.runDeccelAmount;
        else
            accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? _playerData.runAccelAmount * _playerData.accelInAir : _playerData.runDeccelAmount * _playerData.deccelInAir;
        #endregion

        #region Add Bonus Jump Apex Acceleration
        //Increase are acceleration and maxSpeed when at the apex of their jump, makes the jump feel a bit more bouncy, responsive and natural
        if ((IsJumping || IsWallJumping || _isJumpFalling) && Mathf.Abs(rb.velocity.y) < _playerData.jumpHangTimeThreshold)
        {
            accelRate *= _playerData.jumpHangAccelerationMult;
            targetSpeed *= _playerData.jumpHangMaxSpeedMult;
        }
        #endregion

        #region Conserve Momentum
        //We won't slow the player down if they are moving in their desired direction but at a greater speed than their maxSpeed
        if (_playerData.doConserveMomentum && Mathf.Abs(rb.velocity.x) > Mathf.Abs(targetSpeed) && Mathf.Sign(rb.velocity.x) == Mathf.Sign(targetSpeed) && Mathf.Abs(targetSpeed) > 0.01f && LastOnGroundTime < 0)
        {
            //Prevent any deceleration from happening, or in other words conserve are current momentum
            //You could experiment with allowing for the player to slightly increae their speed whilst in this "state"
            accelRate = 0;
        }
        #endregion

        //Calculate difference between current velocity and desired velocity
        float speedDif = targetSpeed - rb.velocity.x;
        //Calculate force along x-axis to apply to thr player

        float movement = speedDif * accelRate;

        //Convert this to a vector and apply to rigidbody
        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);

        /*
		 * For those interested here is what AddForce() will do
		 * rb.velocity = new Vector2(rb.velocity.x + (Time.fixedDeltaTime  * speedDif * accelRate) / rb.mass, rb.velocity.y);
		 * Time.fixedDeltaTime is by default in Unity 0.02 seconds equal to 50 FixedUpdate() calls per second
		*/
    }

    private void Turn()
    {
        //stores scale and flips the player along the x axis, 
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        IsFacingRight = !IsFacingRight;
    }
    #endregion

    #region JUMP METHODS
    private void Jump()
    {
        //Ensures we can't call Jump multiple times from one press
        LastPressedJumpTime = 0;
        LastOnGroundTime = 0;

        #region Perform Jump
        //We increase the force applied if we are falling
        //This means we'll always feel like we jump the same amount 
        //(setting the player's Y velocity to 0 beforehand will likely work the same, but I find this more elegant :D)
        float force = _playerData.jumpForce;
        if (rb.velocity.y < 0)
            force -= rb.velocity.y;

        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

        ParticleSystem particle = _coll.onRightWall ? wallJumpParticle : jumpParticle;
        particle.Play();
        #endregion
    }

    private void WallJump(int dir)
    {
        //Ensures we can't call Wall Jump multiple times from one press
        LastPressedJumpTime = 0;
        LastOnGroundTime = 0;
        LastOnWallRightTime = 0;
        LastOnWallLeftTime = 0;

        #region Perform Wall Jump
        Vector2 force = new Vector2(_playerData.wallJumpForce.x, _playerData.wallJumpForce.y);
        force.x *= dir; //apply force in opposite direction of wall

        if (Mathf.Sign(rb.velocity.x) != Mathf.Sign(force.x))
            force.x -= rb.velocity.x;

        if (rb.velocity.y < 0) //checks whether player is falling, if so we subtract the velocity.y (counteracting force of gravity). This ensures the player always reaches our desired jump force or greater
            force.y -= rb.velocity.y;

        //Unlike in the run we want to use the Impulse mode.
        //The default mode will apply are force instantly ignoring masss
        rb.AddForce(force, ForceMode2D.Impulse);


        ParticleSystem particle = wallJumpParticle;
        particle.Play();
        #endregion
    }
    #endregion

    #region DASH METHODS
    //Dash Coroutine
    private IEnumerator StartDash(Vector2 dir)
    {
        //Overall this method of dashing aims to mimic Celeste, if you're looking for
        // a more physics-based approach try a method similar to that used in the jump
        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.2f, .01f, 14, 90, false, true);
        Camera.main.transform.GetComponent<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));

        anim.SetTrigger("dash");

        LastOnGroundTime = 0;
        LastPressedDashTime = 0;

        float startTime = Time.time;

        _dashesLeft--;
        _isDashAttacking = true;

        SetGravityScale(0);
        _ghost.ShowGhost();
        dashParticle.Play();

        //We keep the player's velocity at the dash speed during the "attack" phase (in celeste the first 0.15s)
        while (Time.time - startTime <= _playerData.dashAttackTime)
        {
            rb.velocity = dir.normalized * _playerData.dashSpeed;
            //Pauses the loop until the next frame, creating something of a Update loop. 
            //This is a cleaner implementation opposed to multiple timers and this coroutine approach is actually what is used in Celeste :D
            yield return null;
        }

        startTime = Time.time;

        _isDashAttacking = false;

        //Begins the "end" of our dash where we return some control to the player but still limit run acceleration (see Update() and Run())
        rb.velocity = Vector2.zero;
        rb.velocity = _playerData.dashEndSpeed * dir.normalized;

        DOVirtual.Float(5, 0, .3f, RigidbodyDrag);

        while (Time.time - startTime <= _playerData.dashEndTime)
        {
            yield return null;
        }

        SetGravityScale(_playerData.gravityScale);
        dashParticle.Stop();

        //Dash over
        IsDashing = false;
    }
    void RigidbodyDrag(float x)
    {
        rb.drag = x;
    }

    //Short period before the player is able to dash again
    private IEnumerator RefillDash(int amount)
    {
        //SHoet cooldown, so we can't constantly dash along the ground, again this is the implementation in Celeste, feel free to change it up
        _dashRefilling = true;
        yield return new WaitForSeconds(_playerData.dashRefillTime);
        _dashRefilling = false;
        _dashesLeft = Mathf.Min(_playerData.dashAmount, _dashesLeft + 1);
    }
    #endregion

    #region OTHER MOVEMENT METHODS
    private void Slide()
    {
        /*
        //Works the same as the Run but only in the y-axis
        //THis seems to work fine, buit maybe you'll find a better way to implement a slide into this system
        float speedDif = _playerData.slideSpeed - rb.velocity.y;
        float movement = speedDif * _playerData.slideAccel;
        //So, we clamp the movement here to prevent any over corrections (these aren't noticeable in the Run)
        //The force applied can't be greater than the (negative) speedDifference * by how many times a second FixedUpdate() is called. For more info research how force are applied to rigidbodies.
        movement = Mathf.Clamp(movement, -Mathf.Abs(speedDif) * (1 / Time.fixedDeltaTime), Mathf.Abs(speedDif) * (1 / Time.fixedDeltaTime));
        rb.AddForce(movement * Vector2.up);
        */
        rb.velocity = new Vector2(rb.velocity.x, -1f);
    }
    #endregion

    #region CHECK METHODS
    public void CheckDirectionToFace(bool isMovingRight)
    {
        if (isMovingRight != IsFacingRight)
            Turn();
    }

    private bool CanJump()
    {
        return LastOnGroundTime > 0 && !IsJumping;
    }

    private bool CanWallJump()
    {
        return LastPressedJumpTime > 0 && LastOnWallTime > 0 && LastOnGroundTime <= 0 && (!IsWallJumping ||
             (LastOnWallRightTime > 0 && _lastWallJumpDir == 1) || (LastOnWallLeftTime > 0 && _lastWallJumpDir == -1));
    }

    private bool CanJumpCut()
    {
        return IsJumping && rb.velocity.y > 0;
    }

    private bool CanWallJumpCut()
    {
        return IsWallJumping && rb.velocity.y > 0;
    }

    private bool CanDash()
    {
        if (!IsDashing && _dashesLeft < _playerData.dashAmount && LastOnGroundTime > 0 && !_dashRefilling)
        {
            StartCoroutine(nameof(RefillDash), 1);
        }

        return _dashesLeft > 0;
    }

    public bool CanSlide()
    {
        if (LastOnWallTime > 0 && !IsJumping && !IsWallJumping && !IsDashing && LastOnGroundTime <= 0)
            return true;
        else
            return false;
    }
    #endregion
}
