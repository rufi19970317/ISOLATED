using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseStat : Stat
{
    UI_HPBar_Object hpBar;
    [SerializeField]
    GameObject Shield;
    void Start()
    {
        Init();
    }

    void Init()
    {
        if (gameObject.GetComponentInChildren<UI_HPBar_Object>() == null)
        {
            hpBar = Managers.UI.MakeWorldSpaceUI<UI_HPBar_Object>(transform);
            hpBar.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);

        }
        _hp = 100;
        _maxHp = 100;
    }
    public override void OnDamaged(int damage)
    {
        if (Hp > 0)
        {
            int _damage = Mathf.Max(0, damage);
            Hp -= _damage;
        }
        
        if (Hp <= 0)
        {
            OnDead();
        }
    }

    protected override void OnDead()
    {
        Hp = 0;
        hpBar.gameObject.SetActive(false);
        Shield.SetActive(false);
    }
}
