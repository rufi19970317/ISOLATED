using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    protected Transform _player;
    protected PlayerStat _playerStat;

    void Start()
    {
        Init();
    }


    #region Init
    protected virtual void Init()
    {
        SetPlayer();
    }

    void SetPlayer()
    {
        _player = Managers.Game.GetPlayer().transform;
        _playerStat = _player.GetComponent<PlayerStat>();
    }
    #endregion
}
