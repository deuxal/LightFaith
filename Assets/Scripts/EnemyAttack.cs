using UnityEngine;
using System.Collections;


public class EnemyAttack : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public float timeBetweenBursts = 2f;
    public int bulletsPerBurst = 3;
    public float timeBetweenBullets = 0.5f;

    private int bulletsShot = 0;

    void Start()
    {
        StartCoroutine(ShootBurst());
    }

    IEnumerator ShootBurst()
    {
        while (true)
        {
            // Shoot a burst of bullets
            for (int i = 0; i < bulletsPerBurst; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
                bulletRb.velocity = transform.up * bulletSpeed;
                bulletsShot++;

                yield return new WaitForSeconds(timeBetweenBullets);
            }

            // Wait for the next burst
            yield return new WaitForSeconds(timeBetweenBursts);

            // Reset the bullet count
            bulletsShot = 0;
        }
    }
}
