using UnityEngine;
using System.Collections;

[AddComponentMenu("Playground/Attributes/Modify Enemy Health")]
public class ModifyEnemyHealthAttribute : MonoBehaviour
{
    public bool destroyWhenActivated = false;
    public int healthChange = -1;

    // This function gets called everytime this object collides with another
    private void OnCollisionEnter2D(Collision2D collisionData)
    {
        OnTriggerEnter2D(collisionData.collider);
    }

    private void OnTriggerEnter2D(Collider2D colliderData)
    {
        EnemyHealthSystem healthScript = colliderData.gameObject.GetComponent<EnemyHealthSystem>();
        if (healthScript != null)
        {
            // Modify health of the enemy
            healthScript.ModifyHealth(healthChange);

            if (destroyWhenActivated)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
