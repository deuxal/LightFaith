using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDestructsOnCollider : MonoBehaviour
{
    public float selfDestructDelay = 0.1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Light"))
        {
            Destroy(gameObject, selfDestructDelay);
        }
    }
}