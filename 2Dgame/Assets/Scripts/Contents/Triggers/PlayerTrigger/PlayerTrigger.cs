using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Item"))
        {
            if (Time.timeScale != 0f)
            {
                if (transform.parent.gameObject.activeSelf)
                {
                    GameObject item = other.transform.parent.gameObject;
                    string itemName = item.name;
                    switch (itemName)
                    {
                        case "WeaponEXP":
                            transform.GetComponentInParent<PlayerStat>().WeaponExp += 1;
                            break;
                        case "PlayerEXP":
                            transform.GetComponentInParent<PlayerStat>().PlayerExp += 1;
                            break;
                        case "HouseEXP":
                            transform.GetComponentInParent<PlayerStat>().HouseExp += 1;
                            break;
                        case "Key":
                            Managers.Game.GetPlayer().GetComponent<PlayerStat>().GetKeyUI().UpdateKey();
                            break;
                    }
                    Managers.Game.Despawn(item);
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Shield")
        {
            if (transform.parent.gameObject.activeSelf)
            {
                transform.GetComponentInParent<WeaponController>().OnAllWeapon();
                transform.GetComponentInParent<PlayerStat>().OnHeal();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Shield")
        {
            if (transform.parent.gameObject.activeSelf)
            {
                transform.GetComponentInParent<WeaponController>().OffAllWeapon();
                transform.GetComponentInParent<PlayerStat>().OffHeal();
            }
        }
    }
}
