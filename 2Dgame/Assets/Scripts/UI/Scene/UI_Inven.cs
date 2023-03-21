using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inven : UI_Scene
{
    Dictionary<Define.Weapon, GameObject> invenDic = new Dictionary<Define.Weapon, GameObject>();
    Queue<Weapon_Inven_Item> _invenQueue = new Queue<Weapon_Inven_Item>();

    Weapon_Inven_Item currentWeapon = new Weapon_Inven_Item(Define.Weapon.None, null, null);

    struct Weapon_Inven_Item
    {
        public Define.Weapon weapon;
        public GameObject go;
        public Sprite image;

        public Weapon_Inven_Item(Define.Weapon Weapon, GameObject Go, Sprite Image)
        {
            this.weapon = Weapon;
            this.go = Go;
            this.image = Image;
        }
    }

    // �κ��丮 UI
    enum GameObjects
    {
        WeaponGridPanel,
        WeaponInventory,
        AllWeapon,
        NoneWeapon
    }

    // ����
    public override void Init()
    {
        base.Init();

        // UI ������Ʈ ���ε�
        Bind<GameObject>(typeof(GameObjects));

        // �׸��� �г� ��������
        GameObject weaponGridPanel = Get<GameObject>((int)GameObjects.WeaponGridPanel);

        // �κ��丮 ������ (�ڽ� ������Ʈ) �ʱ�ȭ
        foreach (Transform child in weaponGridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        Get<GameObject>((int)GameObjects.WeaponInventory).SetActive(false);
        Get<GameObject>((int)GameObjects.AllWeapon).SetActive(false) ;
    }

    // �κ��丮 ������ ����
    public void SetWeaponItem(Define.Weapon weapon)
    {
        GameObject gridPanel = Get<GameObject>((int)GameObjects.WeaponGridPanel);

        // ù ������ ���,
        if(invenDic.Count == 0)
        {
            // weapon ���� �� currentWeapon ����
            Get<GameObject>((int)GameObjects.NoneWeapon).SetActive(false);
            Get<GameObject>((int)GameObjects.WeaponInventory).SetActive(true);

            GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(transform).gameObject;
            item.GetOrAddComponent<UI_Inven_Item>();
            currentWeapon = new Weapon_Inven_Item(weapon, item, Managers.Resource.Load<Sprite>("Sprites/Items/Weapon/" + weapon.ToString()));
            item.SetActive(false);
            invenDic.Add(weapon, item);

            // �̹��� ����
            Get<GameObject>((int)GameObjects.WeaponInventory).transform.Find("WeaponImage").GetComponent<Image>().sprite = currentWeapon.image;

        }
        else if (!invenDic.ContainsKey(weapon))
        {
            // weapon ����
            GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform).gameObject;
            UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();
            Weapon_Inven_Item WeaponSet = new Weapon_Inven_Item(weapon, item, Managers.Resource.Load<Sprite>("Sprites/Items/Weapon/" + weapon.ToString()));

            // inven�� ����
            invenItem.SetImage(WeaponSet.image);
            invenDic.Add(weapon, item);

            _invenQueue.Enqueue(WeaponSet);
        }
    }

    public Define.Weapon ChangeWeapon()
    {
        // �ٲ� ���Ⱑ ���� ��,
        if(_invenQueue.Count > 0)
        {
            // ���� ���⸦ ť�� �ִ´�.
            _invenQueue.Enqueue(currentWeapon);
            currentWeapon.go.transform.parent = Get<GameObject>((int)GameObjects.WeaponGridPanel).transform;
            currentWeapon.go.SetActive(true);

            // ���� ���⸦ ���� ��ü�Ѵ�.
            currentWeapon = _invenQueue.Dequeue();
            Get<GameObject>((int)GameObjects.WeaponInventory).transform.Find("WeaponImage").GetComponent<Image>().sprite = currentWeapon.image;
            currentWeapon.go.transform.parent = transform;

            currentWeapon.go.SetActive(false);
        }
        return currentWeapon.weapon;
    }

    public Define.Weapon AllWeaponUIOn()
    {
        if (_invenQueue.Count > 0)
        {
            Get<GameObject>((int)GameObjects.WeaponGridPanel).SetActive(false);
            Get<GameObject>((int)GameObjects.WeaponInventory).SetActive(false);
            Get<GameObject>((int)GameObjects.AllWeapon).SetActive(true);
            return Define.Weapon.All;
        }
        else
            return Define.Weapon.None;
    }

    public Define.Weapon AllWeaponUIOff()
    {
        Get<GameObject>((int)GameObjects.WeaponGridPanel).SetActive(true);
        Get<GameObject>((int)GameObjects.WeaponInventory).SetActive(true);
        Get<GameObject>((int)GameObjects.AllWeapon).SetActive(false);

        return currentWeapon.weapon;
    }
}