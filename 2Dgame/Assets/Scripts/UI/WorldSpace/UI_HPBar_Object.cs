using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar_Object : UI_Base
{
    enum GameObjects
    {
        HPBar
    }

    Stat _stat;

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        _stat = transform.parent.GetComponent<Stat>();
    }

    private void Update()
    {
        if (transform.parent.gameObject.activeSelf)
        {
            Transform parent = transform.parent;
            transform.position = parent.position + Vector3.up * (parent.GetComponentInChildren<Collider2D>().bounds.size.y - 0.5f);

            float ratio = _stat.Hp / (float)_stat.MaxHp;
            SetHpRatio(ratio);
        }
    }

    public void SetHpRatio(float ratio)
    {
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
    }
}
