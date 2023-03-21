using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : Weapon
{
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * -60f);
    }
}
