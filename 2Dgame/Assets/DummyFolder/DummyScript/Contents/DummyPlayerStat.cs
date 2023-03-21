using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Util;

public class DummyPlayerStat : Stat
{
    /*
    [Space(20)]
    [Header("Weapon Stat")]
    [SerializeField]
    int _weaponDamagePer;
    [SerializeField]
    int _weaponCoolTimePer;
    [SerializeField]
    int _weaponDurationPer;

    [Space(5)]
    [SerializeField]
    float _weaponSpeed;
    [SerializeField]
    float _weaponRange;
    [SerializeField]
    int _weaponNum;

    public int WeaponDamagePer { get { return _weaponDamagePer; } set { _weaponDamagePer = value; } }
    public int WeaponCoolTimePer { get { return _weaponCoolTimePer; } set { _weaponCoolTimePer = value; } }
    public int WeaponDurationPer { get { return _weaponDurationPer; } set { _weaponDurationPer = value; } }
    public float WeaponSpeed { get { return _weaponSpeed; } set { _weaponSpeed = value; } }
    public float WeaponRange { get { return _weaponRange; } set { _weaponRange = value; } }
    public int WeaponNum { get { return _weaponNum; } set { _weaponNum = value; } }

    [Space(20)]
    [Header("Player Stat")]
    [SerializeField]
    int _defense;
    [SerializeField]
    float _jumpPower = 12.5f;
    [SerializeField]
    int _gold;
    [SerializeField]
    float _heal;
    [SerializeField]
    float _magnetRange;
    [SerializeField]
    int _expPer;
    [SerializeField]
    float _luck;

    public int Defense { get { return _defense; } set { _defense = value; } }
    public float JumpPower { get { return _jumpPower; } set { _jumpPower = value; } }
    public int Gold { get { return _gold; } set { _gold = value; } }
    public float Heal { get { return _heal; } set { _heal = value; } }
    public float MagnetRange { get { return _magnetRange; } set { _magnetRange = value; } }
    public int ExpPer { get { return _expPer; } set { _expPer = value; } }
    public float Luck { get { return _luck; } set { _luck = value; } }

    [Space(20)]
    [Header("Player Level")]
    // 플레이어 레벨
    public bool isLevelUp = false;
    [SerializeField]
    int _level;
    [SerializeField]
    int _totalExp;
    [SerializeField]
    int _exp;

    public int Level { get { return _level; } set { _level = value; } }
    public int TotalExp { get { return _totalExp; } set { _totalExp = value; } }

    // 플레이어 레벨업
    public int Exp
    {
        get { return _exp; }
        set
        {
            _exp = value;

            //Data.Stat stat;
            //if (Managers.Data.StatDict.TryGetValue(Level + 1, out stat) == false)
            //break;
            if (_exp >= _totalExp)
            {
                isLevelUp = true;
                Level++;
                Managers.UI.ShowPopUpUI<UI_LevelUP>();
                //SetStat(Level);
                TotalExp += 500;
                _exp = 0;
            }
        }
    }


    void Start()
    {
        _name = gameObject.name;
        _weaponDamagePer = 0;
        _weaponCoolTimePer = 0;
        _exp = 0;
        _defense = 5;
        _gold = 0;
        _jumpPower = 12.5f;

        SetStat(_name);
    }

    public void SetStat(string name)
    {
        Dictionary<string, Data.Stat> dict = Managers.Data.StatDict;
        Data.Stat stat = dict[name];

        _hp = stat.maxHp;
        _maxHp = stat.maxHp;
        _attack = stat.attack;
        _moveSpeed = stat.moveSpeed;
        _totalExp = stat.totalExp;
        transform.GetComponent<WeaponController>().SetWeapon(EnumUtil<Define.Weapon>.Parse(stat.weaponType));
    }

    public override void OnDamaged(int damage)
    {
        if (Hp > 0)
        {
            int _damage = Mathf.Max(0, damage - Defense);
            Hp -= _damage;
        }
        else
        {
            //transform.GetComponent<PlayerController>().State = Define.State.Die;
        }
    }
    */
}
