using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") && gameObject.CompareTag("Light"))
        {
            Destroy(gameObject);
        }
    }
}


