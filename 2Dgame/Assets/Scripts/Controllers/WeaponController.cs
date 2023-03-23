using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class WeaponController : ItemController
{
    public Dictionary<Define.Weapon, string> weaponToString = new Dictionary<Define.Weapon, string>();
    public Dictionary<Define.Weapon, WeaponStat> weaponsStat = new Dictionary<Define.Weapon, WeaponStat>();
    Dictionary<Define.Weapon, float> weaponsTime = new Dictionary<Define.Weapon, float>();

    Transform spawnPool;
    Define.Weapon _currentWeapon = Define.Weapon.None;
    UI_Inven _invenUI;

    void Update()
    {
        if (_currentWeapon != Define.Weapon.None)
        {
            if (weaponsStat.Count != 0)
            {
                if (_currentWeapon != Define.Weapon.All)
                {
                    Spawn(_currentWeapon);
                    weaponsTime[_currentWeapon] -= Time.deltaTime;
                }
                else
                {
                    foreach (KeyValuePair<Define.Weapon, WeaponStat> item in weaponsStat)
                    {
                        Spawn(item.Key);
                        weaponsTime[item.Key] -= Time.deltaTime;
                    }
                }
            }
        }
    }

    GameObject hookParent;
    GameObject skull;
    int hookCount;
    void Spawn(Define.Weapon weapon)
    {
        if (hookParent != null)
        {
            if (_currentWeapon != Define.Weapon.Hook && _currentWeapon != Define.Weapon.All)
                hookParent.SetActive(false);
        }
        if(skull != null)
        {
            if(_currentWeapon != Define.Weapon.Skull && _currentWeapon != Define.Weapon.All)
                skull.SetActive(false);
        }

        switch (weapon)
        {
            #region Hook
            case Define.Weapon.Hook:
                // ���Ⱑ ������ ����
                if (hookParent == null)
                {
                    hookParent = new GameObject("HookParent");
                    hookParent.transform.parent = spawnPool;
                    hookCount = 0;
                }
                else if (!hookParent.activeSelf)
                    hookParent.SetActive(true);
                
                // ���� ���� ä���
                if (weaponsStat[weapon].count != hookCount)
                {
                    hookCount = weaponsStat[weapon].count;

                    for (int i = 0; i < hookCount; i++)
                    {
                        Transform hook;
                        if (i < hookParent.transform.childCount)
                            hook = hookParent.transform.GetChild(i);
                        else
                            hook = Managers.Resource.Instantiate("Weapon/Hook", hookParent.transform).transform;

                        hook.localPosition = Vector3.zero;
                        hook.localRotation = Quaternion.identity;

                        hook.gameObject.GetOrAddComponent<Hook>().SetStat(weapon, weaponsStat[weapon]);
                        Vector3 rotVec = Vector3.forward * 360 * i / hookCount;
                        hook.Rotate(rotVec);
                        hook.Translate(hook.up * 2.5f, Space.World);
                    }

                    hookParent.transform.Translate(new Vector3(0f, -0.4f), Space.Self);
                }
                // ���� ȸ��
                hookParent.transform.Rotate(Vector3.back * weaponsStat[weapon].speed * Time.deltaTime);
                hookParent.transform.position = _player.position - new Vector3(0f, 0.4f);
                break;
            #endregion
            #region Knife
            case Define.Weapon.Knife:
                if (weaponsTime[weapon] <= 0f)
                {
                    GameObject knife = Managers.Resource.Instantiate("Weapon/Knife");
                    knife.GetOrAddComponent<Knife>().SetStat(weapon, weaponsStat[weapon]);
                    knife.transform.position = _player.position;
                    knife.transform.parent = spawnPool;
                    weaponsTime[weapon] = weaponsStat[weapon].coolTime;
                    if(_player.GetComponent<PlayerCollider>().onRightWall)
                        knife.transform.localScale = new Vector3(_player.localScale.x, knife.transform.localScale.y, knife.transform.localScale.z);
                    else
                        knife.transform.localScale = new Vector3(-_player.localScale.x, knife.transform.localScale.y, knife.transform.localScale.z);
                }
                break;
            #endregion
            #region Fireball
            // ȭ����, ���� �հ� ������ ���ư���.
            // ������
            case Define.Weapon.Fireball:
                if (weaponsTime[weapon] <= 0f)
                {
                    if (_player.GetComponent<MonsterScanner>().nearestTarget != null)
                    {
                        GameObject fireball = Managers.Resource.Instantiate("Weapon/Fireball");
                        fireball.GetOrAddComponent<Fireball>().SetStat(weapon, weaponsStat[weapon]);
                        fireball.transform.position = _player.position;
                        fireball.transform.parent = spawnPool;
                        weaponsTime[weapon] = weaponsStat[weapon].coolTime;

                        Vector3 dir = _player.GetComponent<MonsterScanner>().nearestTarget.position - fireball.transform.position;
                        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                        fireball.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    }
                    else if(weaponsTime[weapon] <= -10000f)
                        weaponsTime[weapon] = 0;
                }
                break;
            #endregion
            #region Bomb
            // ���������� ��ź�� ������.
            // ���� �°ų�, ������ ������ �����Ѵ�.
            // ���� �������� �ش�.
            case Define.Weapon.Bomb:
                if (weaponsTime[weapon] <= 0f)
                {
                    GameObject bomb = Managers.Resource.Instantiate("Weapon/Bomb/Bomb");
                    bomb.GetOrAddComponent<Bomb>().SetStat(weapon, weaponsStat[weapon]);
                    bomb.transform.position = _player.position;
                    bomb.transform.parent = spawnPool;
                    bomb.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90 + -_player.transform.localScale.x * 60));
                    bomb.transform.localScale = new Vector2(1, _player.transform.localScale.x);
                    bomb.GetComponent<Bomb>().Shot();
                    weaponsTime[weapon] = weaponsStat[weapon].coolTime;
                }
                break;
            #endregion
            #region Firebomb
            // �ϴÿ��� �������� ȭ����, ��ġ ����
            // ���� ������ �����Ѵ�.
            // �ٴڿ� ���ӵ������� �ִ� ���� ���.
            case Define.Weapon.FireBomb:
                if (weaponsTime[weapon] <= 0f)
                {
                    GameObject fireBomb = Managers.Resource.Instantiate("Weapon/FireBomb/FireBomb");
                    fireBomb.GetOrAddComponent<FireBomb>().SetStat(weapon, weaponsStat[weapon]);

                    float topCoordinate = Camera.main.transform.position.y + Camera.main.orthographicSize - 0.8f;

                    float minX = Camera.main.transform.position.x - (Camera.main.orthographicSize * Camera.main.aspect) + 1f;
                    float maxX = Camera.main.transform.position.x + (Camera.main.orthographicSize * Camera.main.aspect) - 1f;
                    float randomX = Random.Range(minX, maxX);

                    fireBomb.transform.position = new Vector2(randomX, topCoordinate);
                    fireBomb.transform.parent = spawnPool;
                    weaponsTime[weapon] = weaponsStat[weapon].coolTime;
                }
                break;
            #endregion
            #region Club
            // �÷��̾ ���� �������� ����̸� �ֵθ���.
            case Define.Weapon.Club:
                if (weaponsTime[weapon] <= 0f)
                {
                    GameObject club = Managers.Resource.Instantiate("Weapon/Club");
                    club.transform.localScale = new Vector3(_player.localScale.x, club.transform.localScale.y, club.transform.localScale.z);
                    club.transform.GetChild(0).gameObject.GetOrAddComponent<Club>().SetStat(weapon, weaponsStat[weapon]);
                    club.transform.position = _player.position;
                    club.transform.parent = spawnPool;
                    weaponsTime[weapon] = weaponsStat[weapon].coolTime;
                }
                break;
            #endregion
            #region Skull
            // �÷��̾� ������ ���������� ���� �˹��Ű�� ������ ǳ���.
            case Define.Weapon.Skull:
                // ���Ⱑ ������ ����
                if (skull == null)
                {
                    skull = Managers.Resource.Instantiate("Weapon/Skull");
                    skull.GetOrAddComponent<Skull>().SetStat(weapon, weaponsStat[weapon]);
                    skull.transform.parent = spawnPool;
                }
                else if (!skull.activeSelf)
                {
                    skull.SetActive(true);
                }
                skull.transform.position = _player.position;
                break;
            #endregion
            #region Magic
            case Define.Weapon.Magic:
                // �÷��̾� ���濡 �������� �׷�, �� ���� �ִ� ������ �����Ѵ�.
                // ���� ������, ���� ������
                if (weaponsTime[weapon] <= 0f)
                {
                    int _magicLayerMask = 1 << LayerMask.NameToLayer("Platforms");
                    float ls = _player.localScale.x;
                    RaycastHit2D hit = Physics2D.Raycast(_player.position, ls * _player.right, 10f, _magicLayerMask);
                    if(hit.collider == null)
                    {
                        Vector2 _rayPos = _player.position + new Vector3(ls * 10f, 0f, 0f);
                        hit = Physics2D.Raycast(_rayPos, Vector2.down, 1000f, _magicLayerMask);

                        if (hit.collider.CompareTag("Wall"))
                        {
                            GameObject magic = Managers.Resource.Instantiate("Weapon/Magic");
                            magic.GetOrAddComponent<Magic>().SetStat(weapon, weaponsStat[weapon]);
                            magic.transform.position = hit.point;
                            magic.transform.parent = spawnPool;
                            magic.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                        }
                    }
                    else if(hit.collider.CompareTag("Wall"))
                    {
                        GameObject magic = Managers.Resource.Instantiate("Weapon/Magic");
                        magic.GetOrAddComponent<Magic>().SetStat(weapon, weaponsStat[weapon]);
                        magic.transform.position = hit.point;
                        magic.transform.parent = spawnPool;
                        magic.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, ls * 90f));
                    }
                    weaponsTime[weapon] = weaponsStat[weapon].coolTime;
                }
                break;
            #endregion
        }
    }

    #region All Weapon
    void OnAllWeapon()
    {
        if (_currentWeapon != Define.Weapon.None)
            _currentWeapon = _invenUI.AllWeaponUIOn();
    }
    void OffAllWeapon()
    {
        if(_currentWeapon == Define.Weapon.All)
            _currentWeapon = _invenUI.AllWeaponUIOff();
    }
    #endregion

    #region Init Weapon
    protected override void Init()
    {
        base.Init();

        spawnPool = GameObject.Find("WeaponPool").transform;
        if (_invenUI == null)
            _invenUI = Managers.UI.ShowSceneUI<UI_Inven>();
    }
    #endregion

    #region Weapon Set
    public void SetWeapon(Define.Weapon weapon)
    {
        if (weapon != Define.Weapon.None && weapon != Define.Weapon.All)
        {
            if (!weaponsStat.ContainsKey(weapon))
                weaponsStat.Add(weapon, Managers.Data.WeaponLv[0][weapon.ToString()]);

            if (!weaponsTime.ContainsKey(weapon))
                weaponsTime.Add(weapon, 0f);

            if (!weaponToString.ContainsKey(weapon))
                weaponToString.Add(weapon, weapon.ToString());

            if (_invenUI == null)
                _invenUI = Managers.UI.ShowSceneUI<UI_Inven>();

            if (weaponsStat.Count == 1) _currentWeapon = weapon;
            _invenUI.SetWeaponItem(weapon);
        }
    }

    public void SetCurrentWeapon(Define.Weapon weapon)
    {
        _currentWeapon = weapon;
    }

    public int WeaponLevelUp(Define.Weapon weapon)
    {
        if (weaponsStat.ContainsKey(weapon))
            SetWeaponStat(weapon);
        else
            SetWeapon(weapon);

        return weaponsStat[weapon].level;
    }

    public void SetWeaponStat(Define.Weapon weapon)
    {
        if(weaponsStat.ContainsKey(weapon))
        {
            int level = weaponsStat[weapon].level;
            weaponsStat[weapon] = Managers.Data.WeaponLv[level][weapon.ToString()];
            
            UI_Inven _invenUI = GameObject.Find("UI_Inven").GetComponent<UI_Inven>();
            _invenUI.SetWeaponItem(weapon);
        }
    }
    #endregion

    #region KeyEvent
    #region AddEvent
    private void OnEnable()
    {
        // ��ǲ�Ŵ����� �̺�Ʈ �߰�
        Managers.Input.KeyAction -= OnKeyEvent;
        Managers.Input.KeyAction += OnKeyEvent;
    }
    private void OnDisable()
    {
        Managers.Input.KeyAction -= OnKeyEvent;
    }
    #endregion

    void OnKeyEvent(Define.KeyEvent Key)
    {
        if (Time.timeScale != 0f)
        {
            switch (Key)
            {
                case Define.KeyEvent.Select:
                    if(_currentWeapon != Define.Weapon.All)
                        _currentWeapon = _invenUI.ChangeWeapon();
                    break;
                case Define.KeyEvent.Down:
                    OnAllWeapon();
                    break;
                case Define.KeyEvent.Up:
                    OffAllWeapon();
                    break;
                case Define.KeyEvent.Cancel:
                    break;
            }
        }
        else
        {
            switch (Key)
            {
                case Define.KeyEvent.Select:
                    break;
            }
        }
    }
    #endregion
}
