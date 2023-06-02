using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public void OnAnimationEnd()
    {
        Managers.Game.Despawn(gameObject);
    }
}
