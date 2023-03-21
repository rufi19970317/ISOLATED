using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Weapon
{
    public void OnDespawn()
    {
        Managers.Resource.Destroy(gameObject);
    }
}
