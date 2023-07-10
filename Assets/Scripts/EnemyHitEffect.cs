using UnityEngine;

public class EnemyHitEffect : MonoBehaviour
{
    public GameObject hitPrefab; // Reference to the hit prefab
    public float hitDuration = 0.2f; // Duration in seconds for the hit effect

    private GameObject originalObject;
    private bool isHit = false;

    private void Start()
    {
        originalObject = gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !isHit)
        {
            isHit = true;
            GameObject hitEffect = Instantiate(hitPrefab, transform.position, transform.rotation);
            Destroy(hitEffect, hitDuration);
            Invoke(nameof(ResetHitEffect), hitDuration);
        }
    }

    private void ResetHitEffect()
    {
        isHit = false;
    }
}

