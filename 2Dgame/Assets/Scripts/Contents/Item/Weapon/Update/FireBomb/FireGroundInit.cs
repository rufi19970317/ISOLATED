using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGroundInit : MonoBehaviour
{
    List<Vector2> childPos = new List<Vector2>();
    float _weaponTime = 0f;

    void Start()
    {
        foreach (Transform child in transform)
            childPos.Add(child.localPosition);
    }

    void Update()
    {
        _weaponTime -= Time.deltaTime;
        if (_weaponTime <= 0f)
        {
            _weaponTime = 10f;
            OnDespawn();
        }
    }

    void OnDespawn()
    {
        for (int i = 0; i < childPos.Count; i++)
            transform.GetChild(i).localPosition = childPos[i];
        Managers.Resource.Destroy(gameObject);
    }

    public void Init(Define.Weapon weapon, Data.WeaponStat weaponStat)
    {
        foreach (Transform child in transform)
            child.gameObject.GetOrAddComponent<FireGround>().SetStat(weapon, weaponStat);

        _weaponTime = weaponStat.duration;
    }
}
