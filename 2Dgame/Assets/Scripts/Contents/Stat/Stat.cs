using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;



public class Stat : MonoBehaviour
{
    [SerializeField]
    protected string _name;
    [SerializeField]
    protected int _hp;
    [SerializeField]
    protected int _maxHp;
    [SerializeField]
    protected int _attack;
    [SerializeField]
    protected float _moveSpeed;

    public string Name { get { return _name; } set { _name = value; } }
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    protected virtual void OnDead()
    {
        Managers.Game.Despawn(gameObject);
    }

    public virtual void OnDamaged(int damage)
    {
        int _damage = Mathf.Max(0, damage);
        Hp -= _damage;

        if (Hp <= 0)
        {
            Hp = 0;
            Managers.Game.Despawn(gameObject);
        }
    }
}
