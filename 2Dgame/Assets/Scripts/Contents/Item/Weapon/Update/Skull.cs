using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skull : Weapon
{
    void Update()
    {
        transform.position = _player.transform.position;
    }
}
