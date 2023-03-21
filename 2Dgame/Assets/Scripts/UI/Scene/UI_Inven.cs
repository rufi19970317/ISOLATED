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

    // 인벤토리 UI
    enum GameObjects
    {
        WeaponGridPanel,
        WeaponInventory,
        AllWeapon,
        NoneWeapon
    }

    // 세팅
    public override void Init()
    {
        base.Init();

        // UI 오브젝트 바인드
        Bind<GameObject>(typeof(GameObjects));

        // 그리드 패널 가져오기
        GameObject weaponGridPanel = Get<GameObject>((int)GameObjects.WeaponGridPanel);

        // 인벤토리 아이템 (자식 오브젝트) 초기화
        foreach (Transform child in weaponGridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        Get<GameObject>((int)GameObjects.WeaponInventory).SetActive(false);
        Get<GameObject>((int)GameObjects.AllWeapon).SetActive(false) ;
    }

    // 인벤토리 아이템 생성
    public void SetWeaponItem(Define.Weapon weapon)
    {
        GameObject gridPanel = Get<GameObject>((int)GameObjects.WeaponGridPanel);

        // 첫 무기일 경우,
        if(invenDic.Count == 0)
        {
            // weapon 저장 및 currentWeapon 설정
            Get<GameObject>((int)GameObjects.NoneWeapon).SetActive(false);
            Get<GameObject>((int)GameObjects.WeaponInventory).SetActive(true);

            GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(transform).gameObject;
            item.GetOrAddComponent<UI_Inven_Item>();
            currentWeapon = new Weapon_Inven_Item(weapon, item, Managers.Resource.Load<Sprite>("Sprites/Items/Weapon/" + weapon.ToString()));
            item.SetActive(false);
            invenDic.Add(weapon, item);

            // 이미지 변경
            Get<GameObject>((int)GameObjects.WeaponInventory).transform.Find("WeaponImage").GetComponent<Image>().sprite = currentWeapon.image;

        }
        else if (!invenDic.ContainsKey(weapon))
        {
            // weapon 생성
            GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform).gameObject;
            UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();
            Weapon_Inven_Item WeaponSet = new Weapon_Inven_Item(weapon, item, Managers.Resource.Load<Sprite>("Sprites/Items/Weapon/" + weapon.ToString()));

            // inven에 저장
            invenItem.SetImage(WeaponSet.image);
            invenDic.Add(weapon, item);

            _invenQueue.Enqueue(WeaponSet);
        }
    }

    public Define.Weapon ChangeWeapon()
    {
        // 바꿀 무기가 있을 때,
        if(_invenQueue.Count > 0)
        {
            // 현재 무기를 큐에 넣는다.
            _invenQueue.Enqueue(currentWeapon);
            currentWeapon.go.transform.parent = Get<GameObject>((int)GameObjects.WeaponGridPanel).transform;
            currentWeapon.go.SetActive(true);

            // 다음 무기를 꺼내 교체한다.
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