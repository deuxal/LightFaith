using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSelfDestructs : MonoBehaviour
{
    public float lifetime = 3f; // Time in seconds before the projectile self-destructs

    private void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    public float selfDestructDelay = 0.1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject, selfDestructDelay);
    }
}
