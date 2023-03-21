using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Util;

public class PlayerStat : MonoBehaviour
{
    #region Player Parameters
    [Space(20)]
    [Header("Player Stat")]
    [SerializeField]
    int _hp;
    [SerializeField]
    int _maxHp;
    [SerializeField]
    int _gold;
    [SerializeField]
    float _magnetRange;

    public int Hp
    {
        get { return _hp; }
        set
        {
            _hp = value;
            hpBar.UpdateHP((float) _hp / _maxHp);
        }
    }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Gold { get { return _gold; } set { _gold = value; } }
    public float MagnetRange { get { return _magnetRange; } set { _magnetRange = value; } }

    [Space(20)]
    [Header("Player Level")]
    // 플레이어 레벨
    [SerializeField]
    int _weaponUpgradeNum;
    int _playerUpgradeNum;
    int _houseUpgradeNum;

    [SerializeField]
    int _totalWeaponExp;
    [SerializeField]
    int _totalPlayerExp;
    [SerializeField]
    int _totalHouseExp;

    [SerializeField]
    int _weaponExp;
    [SerializeField]
    int _playerExp;
    [SerializeField]
    int _houseExp;

    public int WeaponUpgradeNum { get { return _weaponUpgradeNum; } set { _weaponUpgradeNum = value; } }
    public int PlayerUpgradeNum { get { return _playerUpgradeNum; } set { _playerUpgradeNum = value; } }
    public int HouseUpgradeNum { get { return _houseUpgradeNum; } set { _houseUpgradeNum = value; } }

    public int TotalWeaponExp { get { return _totalWeaponExp; } set { _totalWeaponExp = value; } }
    public int TotalPlayerExp { get { return _totalPlayerExp; } set { _totalPlayerExp = value; } }
    public int TotalHouseExp { get { return _totalHouseExp; } set { _totalHouseExp = value; } }


    // 플레이어 레벨업
    public int WeaponExp
    {
        get { return _weaponExp; }
        set
        {
            _weaponExp = value;

            //Data.Stat stat;
            //if (Managers.Data.StatDict.TryGetValue(Level + 1, out stat) == false)
            //break;
            if (_weaponExp >= _totalWeaponExp)
            {
                _weaponExp = 0;
                _weaponUpgradeNum++;
            }

            expBar.UpdateEXP(Define.EXPType.WeaponEXP, _weaponUpgradeNum, (float)_weaponExp / _totalWeaponExp);
        }
    }

    public int PlayerExp
    {
        get { return _playerExp; }
        set
        {
            _playerExp = value;

            //Data.Stat stat;
            //if (Managers.Data.StatDict.TryGetValue(Level + 1, out stat) == false)
            //break;
            if (_playerExp >= _totalPlayerExp)
            {
                _playerExp = 0;
                _playerUpgradeNum++;
            }
            expBar.UpdateEXP(Define.EXPType.PlayerEXP, _playerUpgradeNum, (float)_playerExp / _totalPlayerExp);
        }
    }

    public int HouseExp
    {
        get { return _houseExp; }
        set
        {
            _houseExp = value;

            //Data.Stat stat;
            //if (Managers.Data.StatDict.TryGetValue(Level + 1, out stat) == false)
            //break;
            if (_houseExp >= _totalHouseExp)
            {
                _houseExp = 0;
                _houseUpgradeNum++;
            }
            expBar.UpdateEXP(Define.EXPType.HouseEXP, _houseUpgradeNum, (float)_playerExp / _totalHouseExp);
        }
    }

    UI_EXP expBar;
    UI_HPBar hpBar;
    #endregion

    public void SetStat()
    {
        expBar = Managers.UI.ShowSceneUI<UI_EXP>();
        hpBar = Managers.UI.ShowSceneUI<UI_HPBar>();

        Hp = 100;
        MaxHp = 100;

        WeaponUpgradeNum = 0;
        PlayerUpgradeNum = 0;
        HouseUpgradeNum = 0;

        TotalWeaponExp = 20;
        TotalPlayerExp = 5;
        TotalHouseExp = 5;

        WeaponExp = 0;
        PlayerExp = 0;
        HouseExp = 0;
    }

    public void OnDamaged(int damage)
    {
        if (Hp > 0)
        {
            int _damage = Mathf.Max(0, damage);
            Hp -= _damage;
        }
        else
        {
            //transform.GetComponent<PlayerController>().State = Define.State.Die;
        }
    }
}
