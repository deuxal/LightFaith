using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileForce = 10f;
    public float shootCooldown = 0.5f;
    public Sprite projectileModeCursorSprite;

    private bool isShootingEnabled = false;
    private bool isCooldownActive = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isShootingEnabled = true;
            Vector2 hotspot = new Vector2(projectileModeCursorSprite.texture.width / 2, projectileModeCursorSprite.texture.height / 2);
            Cursor.SetCursor(projectileModeCursorSprite.texture, hotspot, CursorMode.Auto);
            // Cursor.SetCursor(projectileModeCursorSprite.texture, new Vector2(0.5f, 0.5f), CursorMode.Auto);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isShootingEnabled = false;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        if (isShootingEnabled && Input.GetMouseButtonDown(0) && !isCooldownActive)
        {
            ShootProjectile();
            StartCoroutine(StartCooldown());
        }
    }

    private void ShootProjectile()
    {
        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        Vector3 direction = (mouseWorldPosition - projectileSpawnPoint.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(direction * projectileForce, ForceMode2D.Impulse);

        // Add screen shake here
        StartCoroutine(ScreenShake(0.2f, 0.1f));
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }

    private IEnumerator StartCooldown()
    {
        isCooldownActive = true;
        yield return new WaitForSeconds(shootCooldown);
        isCooldownActive = false;
    }

    private IEnumerator ScreenShake(float duration, float intensity)
    {
        Vector3 originalCameraPosition = Camera.main.transform.position;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * intensity;
            float y = Random.Range(-1f, 1f) * intensity;

            Camera.main.transform.position = originalCameraPosition + new Vector3(x, y, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.position = originalCameraPosition;
    }
}
