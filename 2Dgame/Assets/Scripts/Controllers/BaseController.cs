using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{


    [SerializeField]
    protected GameObject _lockTarget;

    [SerializeField]
    protected Vector3 _destPos;

    public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;


    private void Start()
    {
        Init();
    }


    public abstract void Init();
    protected virtual void UpdateDie() { }
    protected virtual void UpdateMoving() { }
    protected virtual void UpdateIdle() { }
    protected virtual void UpdateSkill() { }
}
