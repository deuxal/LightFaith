using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletCountUI : MonoBehaviour
{
    public Text bulletCountText;
    public Projectile projectile;

    private void Start()
    {
        UpdateBulletCount();
        projectile.AddBulletCountChangeListener(UpdateBulletCount);
    }

    private void UpdateBulletCount()
    {
        bulletCountText.text = "Bullets: " + projectile.ammoCount.ToString();
    }
}

