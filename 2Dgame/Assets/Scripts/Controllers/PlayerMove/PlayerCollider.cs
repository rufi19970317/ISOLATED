using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [Header("Layers & Tags")]
    [SerializeField] private LayerMask _groundLayer;

    #region CHECK PARAMETERS
    //Set all of these up in the inspector
    [Header("Checks")]
    [SerializeField] private Transform _groundCheckPoint;
    //Size of groundCheck depends on the size of your character generally you want them slightly small than width (for ground) and height (for the wall check)
    [SerializeField] private Vector2 _groundCheckSize = new Vector2(0.49f, 0.03f);
    [Space(5)]
    [SerializeField] private Transform _frontWallCheckPoint;
    [SerializeField] private Transform _backWallCheckPoint;
    [SerializeField] private Vector2 _wallCheckSize = new Vector2(0.5f, 1f);
    #endregion

    PlayerController _player;
    public bool onGround;
    public bool onWall;
    public bool isRightWall;
    public bool isLeftWall;
    public bool onRightWall;
    public bool onLeftWall;

    void Start()
    {
        _player = Managers.Game.GetPlayer().GetComponent<PlayerController>();
    }

    void Update()
    {
        onGround = Physics2D.OverlapBox(_groundCheckPoint.position, _groundCheckSize, 0, _groundLayer);

        isRightWall = ((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && _player.IsFacingRight)
                    || (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && !_player.IsFacingRight));
        isLeftWall = ((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && !_player.IsFacingRight)
                || (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && _player.IsFacingRight));

        onRightWall = ((Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && _player.IsFacingRight)
                    || (Physics2D.OverlapBox(_frontWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && !_player.IsFacingRight));

        onLeftWall = ((Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && !_player.IsFacingRight)
                || (Physics2D.OverlapBox(_backWallCheckPoint.position, _wallCheckSize, 0, _groundLayer) && _player.IsFacingRight));

        onWall = onRightWall || onLeftWall;

    }

    #region EDITOR METHODS
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_groundCheckPoint.position, _groundCheckSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(_frontWallCheckPoint.position, _wallCheckSize);
        Gizmos.DrawWireCube(_backWallCheckPoint.position, _wallCheckSize);
    }
    #endregion
}
