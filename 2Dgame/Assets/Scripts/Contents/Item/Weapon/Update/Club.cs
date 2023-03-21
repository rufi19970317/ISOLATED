using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Data;

public class Club : Weapon
{
    bool isRotating = false;
    Vector3 angle;

    void Update()
    {
        transform.parent.position = _player.transform.position;
        if (isRotating)
            OnDespawn();
    }

    void OnDespawn()
    {
        isRotating = false;
        transform.parent.eulerAngles = angle;
        Managers.Resource.Destroy(transform.parent.gameObject);
    }

    public override void SetStat(Define.Weapon weaponName, WeaponStat weaponStat)
    {
        base.SetStat(weaponName, weaponStat);

        angle = transform.parent.eulerAngles;
        transform.parent.DORotate(new Vector3(0f, 0f, transform.eulerAngles.z - transform.parent.localScale.x * 90f), _weaponStat.duration)
                        .SetEase(Ease.InOutBack)
                        .OnComplete(() => isRotating = true);
    }
}