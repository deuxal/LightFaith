using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ProjectileDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Light2D light = collision.GetComponent<Light2D>();
        if (light != null)
        {
            Destroy(light.gameObject);
            Destroy(gameObject); // Destroy the projectile as well if desired
        }
    }
}

