using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Item"))
        {
            if (Time.timeScale != 0f)
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
                }
                Managers.Game.Despawn(item);
            }
        }
    }
}
